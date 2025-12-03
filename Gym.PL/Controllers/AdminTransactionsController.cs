using Gym.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTransactionsController : Controller
    {
        private readonly IPaymentService paymentService;
        public AdminTransactionsController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
        public IActionResult Index()
        {
            var transactions = paymentService.GetAllPayments();
            return View(transactions.Item3);
        }
    }
}
