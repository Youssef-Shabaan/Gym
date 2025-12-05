using Gym.BLL.ModelVM.Plan;
using Gym.BLL.Service.Abstraction;
using Gym.BLL.Service.Implementation;
using Gym.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService planService;
        private readonly IMemberService memberService;
        private readonly UserManager<User> userManager;
        private readonly IMemberPlanService memberPlanService;
        private readonly string paypalClientId = "";
        public PlanController(IPlanService planService, 
            IConfiguration configuration,
            IMemberService memberService,
            UserManager<User> userManager,
            IMemberPlanService memberPlanService
            )
        {
            this.planService = planService;
            this.memberService = memberService;
            this.userManager = userManager;
            this.memberPlanService = memberPlanService;
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

        [HttpGet]
        public IActionResult MyPlans()
        {
            var userId = userManager.GetUserId(User);
            var member = memberService.GetByUserID(userId).Item3;
            var memberId = member.MemberId;
            var plans = memberPlanService.GetPlanForMember(memberId);
            return View(plans.Item3);
        }

        [HttpGet]
        public IActionResult GetPlanSessions(int planId)
        {
            var sessionsPlan = planService.GetPlanSessions(planId);
            if(!sessionsPlan.Item1 && sessionsPlan.Item2 == "Plan not found")
            {
                return NotFound();
            }
            return View(sessionsPlan.Item3);
        }
    }
}
