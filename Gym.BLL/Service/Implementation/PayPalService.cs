
using Gym.BLL.Helper;
using Gym.BLL.Service.Abstraction;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Nodes;
using System.Text;

namespace Gym.BLL.Service.Implementation
{
    public class PayPalService : IPayPalService
    {
        private readonly IConfiguration Configuration;
        private readonly PayPal Paypal;
        public PayPalService(IConfiguration configuration)
        {
            Configuration = configuration;
            Paypal = new PayPal
            {
                PayPalClientId = Configuration["PayPalSettings:ClientId"],
                PayPalSecret = Configuration["PayPalSettings:Secret"],
                PayPalUrl = Configuration["PayPalSettings:URL"]
            };
        }
        public async Task<string> GetAccessTokenAsync()
        {
            string accessToken = "";

            string url = Paypal.PayPalUrl + "/v1/oauth2/token";

            using (var client = new HttpClient())
            {
                string credentials64 = Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(Paypal.PayPalClientId + ":" + Paypal.PayPalSecret));

                client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentials64);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

                var httpResponse = await client.SendAsync(requestMessage);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var strResponse = await httpResponse.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);

                    if (jsonResponse != null)
                        accessToken = jsonResponse["access_token"]?.ToString() ?? "";
                }
            }

            return accessToken;
        }
        public async Task<string> CreateOrderAsync(string totalAmount)
        {
            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken))
                return "";

            var url = Paypal.PayPalUrl + "/v2/checkout/orders";

            JsonObject createOrderRequest = new JsonObject
            {
                ["intent"] = "CAPTURE",
                ["purchase_units"] = new JsonArray
        {
            new JsonObject
            {
                ["amount"] = new JsonObject
                {
                    ["currency_code"] = "USD",
                    ["value"] = totalAmount
                }
            }
        }
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(createOrderRequest.ToString(), Encoding.UTF8, "application/json")
                };

                var httpResponse = await client.SendAsync(requestMessage);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var strResponse = await httpResponse.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);
                    return jsonResponse?["id"]?.ToString() ?? "";
                }
            }
            return "";
        }
        public async Task<bool> CompleteOrderAsync(string orderId)
        {
            string accessToken = await GetAccessTokenAsync();
            var url = Paypal.PayPalUrl + "/v2/checkout/orders/" + orderId + "/capture";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent("", Encoding.UTF8, "application/json")
                };

                var httpResponse = await client.SendAsync(requestMessage);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var strResponse = await httpResponse.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);
                    var status = jsonResponse?["status"]?.ToString() ?? "";
                    return status.Equals("COMPLETED", StringComparison.OrdinalIgnoreCase);
                }
            }
            return false;
        }
    }
}
