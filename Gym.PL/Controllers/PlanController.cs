using Gym.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService planService;
        public PlanController(IPlanService planService)
        {
            this.planService = planService;
        }
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
