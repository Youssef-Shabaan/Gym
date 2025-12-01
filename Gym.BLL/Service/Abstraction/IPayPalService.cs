
namespace Gym.BLL.Service.Abstraction
{
    public interface IPayPalService
    {
        Task<string> GetAccessTokenAsync();
        Task<string> CreateOrderAsync(string totalAmount);
        Task<(bool, string?)> CompleteOrderAsync(string orderId);
    }
}