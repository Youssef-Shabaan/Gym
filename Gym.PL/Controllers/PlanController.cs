using Gym.BLL.ModelVM.Plan;
using Gym.BLL.Service.Abstraction;
using Gym.BLL.Service.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService planService;
        private readonly string paypalClientId = "";
        public PlanController(IPlanService planService, IConfiguration configuration)
        {
            this.planService = planService;
            paypalClientId = configuration["PayPalSettings:ClientId"];
        }
        public IActionResult Index()
        {
            var plans = planService.GetAllPlans();
            if (!plans.Item1)
            {
                ViewBag.ErrorMessage = plans.Item2;
                return View(new List<GetPlanVM>());
            }
            return View(plans.Item3);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var plan = planService.GetPlanById(id).Item2;
            if (plan == null)
            {
                return NotFound();
            }
            ViewBag.PlanSession = planService.GetPlanSessions(id).Item3;
            ViewBag.PayPalClientId = paypalClientId;
            ViewBag.TotalAmount = plan.Price.ToString(System.Globalization.CultureInfo.InvariantCulture);
            return View(plan);
        }
    }
}
