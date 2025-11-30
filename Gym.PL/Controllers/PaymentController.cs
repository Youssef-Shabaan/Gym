using AutoMapper;
using Gym.BLL.Helper;
using Gym.BLL.Service.Abstraction;
using Gym.BLL.Service.Implementation;
using Gym.DAL.Entities;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json.Nodes;

namespace Gym.PL.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPayPalService paypalService;
        private readonly PayPal Paypal;
        private readonly IMapper mapper;

        public PaymentController(IMapper mapper, IPayPalService paypalService, IConfiguration configuration)
        {
            this.paypalService = paypalService;
            this.mapper = mapper;
            Paypal = new PayPal
            {
                PayPalClientId = configuration["PayPalSettings:ClientId"],
                PayPalSecret = configuration["PayPalSettings:Secret"],
                PayPalUrl = configuration["PayPalSettings:URL"]
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetPayPalAccessToken()
        {
            var token = await paypalService.GetAccessTokenAsync();
            if (string.IsNullOrEmpty(token))
                return BadRequest("Failed to get PayPal Access Token.");

            return Ok(new { AccessToken = token });
        }
        [HttpPost]
        public async Task<JsonResult> CreateOrder([FromBody] JsonObject data)
        {
            var totalAmount = data?["total"]?.ToString();
            if (string.IsNullOrEmpty(totalAmount))
                return Json(new { error = "Total amount is required." });

            var createOrderRequest = new JsonObject
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

            var accessToken = await paypalService.GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken))
                return Json(new { error = "Failed to get PayPal Access Token" });

            var url = Paypal.PayPalUrl + "/v2/checkout/orders";

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
                    if (jsonResponse != null)
                    {
                        string paypalOrderId = jsonResponse["id"]?.ToString() ?? "";
                        return new JsonResult(new { id = paypalOrderId });
                    }
                }
            }

            return new JsonResult(new { Id = "" });
        }
        [HttpPost]
        public async Task<JsonResult> CompleteOrder([FromBody] JsonObject data)
        {
            var orderid = data?["orderID"]?.ToString();
            if (orderid == null) return new JsonResult("error");

            bool completed = await paypalService.CompleteOrderAsync(orderid);

            if (!completed)
                return new JsonResult("error");

            // logic

            // ====

            return new JsonResult("success");
        }

    }
}
