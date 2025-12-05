using Gym.BLL.ModelVM.Plan;
using Gym.BLL.ModelVM.Session;
using Gym.BLL.Service.Abstraction;
using Gym.BLL.Service.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPlanController : Controller
    {
        private readonly ITrainerService trainerService;
        private readonly IPlanService planService;
        private readonly IMemberPlanService memberPlanService;
        private readonly ISessionService sessionService;
        public AdminPlanController(IPlanService planService , ITrainerService trainerService , IMemberPlanService memberPlanService, ISessionService sessionService)
        {
            this.planService = planService;
            this.trainerService = trainerService;
            this.memberPlanService = memberPlanService;
            this.sessionService = sessionService;
        }
        public IActionResult Index()
        {
            var plans = planService.GetAllPlans().Item3;
            return View(plans);
        }
        [HttpGet]
        public IActionResult Create()
        {
            AddPlanVM addPlan = new AddPlanVM();
            var trainers = trainerService.GetAll();
            if (!trainers.Item1)
            {
                ViewBag.ErrorMessage = trainers.Item2;
                return View(new AddSessionVM());
            }
            addPlan.Trainers = trainers.Item3;
            return View(addPlan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddPlanVM addPlan)
        {
            addPlan.Trainers = trainerService.GetAll().Item3;
            if (ModelState.IsValid)
            {
                var result = planService.CreatePlan(addPlan);
                if (!result.Item1)
                {
                    ViewBag.ErrorMessage = result.Item2;
                    return View(addPlan);
                }
                return RedirectToAction("Index");
            }
            return View(addPlan);
        }
        [HttpGet]
        public IActionResult GetMembersForPlan(int id) {
            var result = memberPlanService.GetMembersForPlan(id);
            if (!result.Item1)
            {
                TempData["ErrorMessage"] = result.Item2;
                return RedirectToAction("Index");
            }
            return View(result.Item3);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var plan = planService.GetPlanById(Id);
            if (!plan.Item1)
            {
                return NotFound();
            }
            var updatedPlan = new UpdatePlanVM()
            {
                Id = plan.Item2.Id,
                Name = plan.Item2.Name,
                Description = plan.Item2.Description,
                StartDate = plan.Item2.StartDate,
                EndDate = plan.Item2.EndDate,
                Price = plan.Item2.Price,
                Capcity = plan.Item2.Capcity,
            };
            return View(updatedPlan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdatePlanVM updatedPlan)
        {
            if (ModelState.IsValid)
            {
                var result = planService.UpdatePlan(updatedPlan);
                if (!result.Item1)
                {
                    ModelState.AddModelError("", result.Item2);
                    return View(updatedPlan);
                }
                return RedirectToAction("Index");
            }
            return View(updatedPlan);
        }

        [HttpGet]
        public IActionResult GetSessionsForPlan(int id) { 
            var sessions = sessionService.GetSessionforPlan(id);
            if (!sessions.Item1)
            {
                TempData["ErrorMessage"] = sessions.Item2;
                return RedirectToAction("Index");
            }
            return View(sessions.Item3);
        }

        [HttpGet]
        public IActionResult AddSessiontoPlan(int id) {
            var planResult = planService.GetPlanById(id);
            if (!planResult.Item1 || planResult.Item2 == null)
            {
                return NotFound();
            }
            var plan = planResult.Item2;
            var newsession = new AddSessionVM() {
                TrainerId = plan.TrainerId,
                Price = plan.Price,
                Capactiy = plan.Capcity,
                PlanId = id
            };
            return View(newsession);
        }
        [HttpPost]
        public IActionResult AddSessiontoPlan(AddSessionVM newsession)
        {
            if (ModelState.IsValid)
            {
                
                var result = sessionService.AddSession(newsession);
                if (!result.Item1)
                {
                    ViewBag.ErrorMessage = result.Item2;
                    return View(newsession);
                }
                return RedirectToAction("Index");
            }
            return View(newsession);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var plan = planService.GetPlanById(id);
            if (!plan.Item1)
            {
                return NotFound();
            }
            var result = planService.DeletePlan(id);
            if (!result.Item1)
            {
                TempData["ErrorMessage"] = result.Item2;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DeleteSession(int Id)
        {
            var session = sessionService.GetById(Id);
            var planid = session.Item2.PlanId;
            if (!session.Item1)
            {
                return NotFound();
            }
            var result = sessionService.Delete(Id);
            if (!result.Item1)
            {
                TempData["ErrorMessage"] = result.Item2;
            }
            return RedirectToAction("GetSessionsForPlan", new { id = planid });
        }
        [HttpGet]
        public IActionResult EditSessioninPlan(int Id)
        {
            var session = sessionService.GetById(Id);
            if (!session.Item1)
            {
                return NotFound();
            }
            var updatedSession = new UpdateSessionVM()
            {
                Id = session.Item2.Id,
                Name = session.Item2.Name,
                Description = session.Item2.Description,
                StartTime = session.Item2.StartTime,
                EndTime = session.Item2.EndTime,
                Price = session.Item2.Price,
                Capactiy = session.Item2.Capactiy,
                PlanId = session.Item2.PlanId
            };
            return View(updatedSession);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSessioninPlan(UpdateSessionVM updatedSession)
        {
            if (ModelState.IsValid)
            {
                var planid = updatedSession.PlanId;
                var result = sessionService.Update(updatedSession);
                if (!result.Item1)
                {
                    ModelState.AddModelError("", result.Item2);
                    return View(updatedSession);
                }
                return RedirectToAction("GetSessionsForPlan", new { id = planid });
            }
            return View(updatedSession);
        }
    }
}
