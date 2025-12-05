using Gym.BLL.ModelVM.Plan;
using Gym.BLL.ModelVM.Session;
using Gym.BLL.Service.Abstraction;
using Gym.BLL.Service.Implementation;
using Gym.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    [Authorize(Roles = "Trainer")]
    public class TrainerPlanController : Controller
    {
        private readonly IPlanService planService;
        private readonly ISessionService sessionService;
        private readonly IMemberService memberService;
        private readonly IMemberPlanService memberPlanService;
        private readonly ITrainerService trainerService;
        private readonly UserManager<User> userManager;
        public TrainerPlanController(ITrainerService trainerService, UserManager<User> userManager, IPlanService planService, ISessionService sessionService, IMemberService memberService, IMemberPlanService memberPlanService)
        {
            this.planService = planService;
            this.sessionService = sessionService;
            this.memberService = memberService;
            this.memberPlanService = memberPlanService;
            this.userManager = userManager;
            this.trainerService = trainerService;
        }
        public IActionResult Index()
        {
            var plans = planService.GetAllPlans().Item3;
            return View(plans);
        }
        public async Task<IActionResult> MyPlans()
        {
            var user = await userManager.GetUserAsync(User);
            var TrainerId = trainerService.GetByUserID(user.Id).Item3.TrainerId;

            var plans = planService.GetPlanByTrainerId(TrainerId).Item2;
            return View(plans);
        }
        [HttpGet]
        public IActionResult GetSessionsForPlan(int id)
        {
            var sessions = sessionService.GetSessionforPlan(id);
            if (!sessions.Item1)
            {
                TempData["ErrorMessage"] = sessions.Item2;
                return RedirectToAction("Index");
            }
            return View(sessions.Item3);
        }
        [HttpGet]
        public async Task<IActionResult> GetSessionsForMyPlan(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var TrainerId = trainerService.GetByUserID(user.Id).Item3.TrainerId;

            var plan = planService.GetPlanById(id);
            if (!plan.Item1 || plan.Item2.TrainerId != TrainerId)
            {
                return NotFound();
            }
            var sessions = sessionService.GetSessionforPlan(id);
            if (!sessions.Item1)
            {
                TempData["ErrorMessage"] = sessions.Item2;
                return RedirectToAction("MyPlans");
            }
            return View(sessions.Item3);
        }
        [HttpGet]
        public IActionResult GetMembersForPlan(int id)
        {
            var result = memberPlanService.GetMembersForPlan(id);
            if (!result.Item1)
            {
                TempData["ErrorMessage"] = result.Item2;
                return RedirectToAction("Index");
            }
            return View(result.Item3);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await userManager.GetUserAsync(User);
            var TrainerId = trainerService.GetByUserID(user.Id).Item3.TrainerId;

            AddPlanVM addPlan = new AddPlanVM();
            
            addPlan.TrainerId = TrainerId;
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
                return RedirectToAction("MyPlans");
            }
            return View(addPlan);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var user = await userManager.GetUserAsync(User);
            var TrainerId = trainerService.GetByUserID(user.Id).Item3.TrainerId;

            var plan = planService.GetPlanById(Id);
            if (!plan.Item1||plan.Item2.TrainerId!=TrainerId)
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
                return RedirectToAction("MyPlans");
            }
            return View(updatedPlan);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var TrainerId = trainerService.GetByUserID(user.Id).Item3.TrainerId;

            var plan = planService.GetPlanById(id);
            if (!plan.Item1 || plan.Item2.TrainerId!=TrainerId)
            {
                return NotFound();
            }
            var result = planService.DeletePlan(id);
            if (!result.Item1)
            {
                TempData["ErrorMessage"] = result.Item2;
            }
            return RedirectToAction("MyPlans");
        }
        [HttpGet]
        public async Task< IActionResult> AddSessiontoPlan(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var TrainerId = trainerService.GetByUserID(user.Id).Item3.TrainerId;

            var planResult = planService.GetPlanById(id);
            if (!planResult.Item1 || planResult.Item2 == null || planResult.Item2.TrainerId!=TrainerId)
            {
                return NotFound();
            }
            var plan = planResult.Item2;
            var newsession = new AddSessionVM()
            {
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
                return RedirectToAction("MyPlans");
            }
            return View(newsession);
        }
        [HttpGet]
        public async Task<IActionResult> EditSessioninPlan(int Id)
        {
            var user = await userManager.GetUserAsync(User);
            var TrainerId = trainerService.GetByUserID(user.Id).Item3.TrainerId;

            var session = sessionService.GetById(Id);
            if (!session.Item1 || session.Item2.TrainerId!=TrainerId)
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
                return RedirectToAction("GetSessionsForMyPlan", new { id = planid });
            }
            return View(updatedSession);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSession(int Id)
        {
            var user = await userManager.GetUserAsync(User);
            var TrainerId = trainerService.GetByUserID(user.Id).Item3.TrainerId;

            var session = sessionService.GetById(Id);
            if (!session.Item1 || session.Item2.TrainerId != TrainerId)
            {
                return NotFound();
            }
            var planid = session.Item2.PlanId;
         
            var result = sessionService.Delete(Id);
            if (!result.Item1)
            {
                TempData["ErrorMessage"] = result.Item2;
            }
            return RedirectToAction("GetSessionsForPlan", new { id = planid });
        }
    }
}
