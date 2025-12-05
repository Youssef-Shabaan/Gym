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
    public class TrainerSessionController : Controller
    {
        private readonly ISessionService sessionService;
        private readonly IMemberSessionService memberSessionService;
        private readonly ITrainerService trainerService;
        private readonly UserManager<User> userManager;

        public TrainerSessionController(ITrainerService trainerService, UserManager<User> userManager, ISessionService sessionService, IMemberSessionService memberSessionService)
        {
            this.sessionService = sessionService;
            this.memberSessionService = memberSessionService;
            this.userManager = userManager;
            this.trainerService = trainerService;
        }
        public IActionResult Index()
        {
            var sessions = sessionService.GetAll();
            if (!sessions.Item1)
            {
                TempData["ErrorMessage"] = sessions.Item2;
            }
            return View(sessions.Item3);
        }
        [HttpGet]
        public IActionResult GetMembersForSession(int SessionId)
        {
            var members = memberSessionService.GetMembersForSession(SessionId);
            if (!members.Item1 && members.Item2 == "Session not found")
            {
                return NotFound();
            }
            return View(members.Item3);
        }
        [HttpGet]
        public async Task<IActionResult> MySessions()
        {
            var user = await userManager.GetUserAsync(User);
            var TrainerId = trainerService.GetByUserID(user.Id).Item3.TrainerId;

            var sessions = sessionService.GetSessionsByTrainerId(TrainerId);
            if (!sessions.Item1)
            {
                TempData["ErrorMessage"] = sessions.Item2;
            }
            return View(sessions.Item3);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var user = await userManager.GetUserAsync(User);
            var TrainerId = trainerService.GetByUserID(user.Id).Item3.TrainerId;

            AddSessionVM addSession = new AddSessionVM();

            addSession.TrainerId = TrainerId;
            return View(addSession);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddSessionVM addSession)
        {
            addSession.Trainers = trainerService.GetAll().Item3;
            if (ModelState.IsValid)
            {
                var result = sessionService.AddSession(addSession);
                if (!result.Item1)
                {
                    ViewBag.ErrorMessage = result.Item2;
                    return View(addSession);
                }
                return RedirectToAction("MySessions");
            }
            return View(addSession);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var user = await userManager.GetUserAsync(User);
            var TrainerId = trainerService.GetByUserID(user.Id).Item3.TrainerId;

            var session = sessionService.GetById(Id);
            if (!session.Item1 || session.Item2.TrainerId != TrainerId)
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
            };
            return View(updatedSession);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateSessionVM updatedSession)
        {
            if (ModelState.IsValid)
            {
                var result = sessionService.Update(updatedSession);
                if (!result.Item1)
                {
                    ModelState.AddModelError("", result.Item2);
                    return View(updatedSession);
                }
                return RedirectToAction("MySessions");
            }
            return View(updatedSession);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var user = await userManager.GetUserAsync(User);
            var TrainerId = trainerService.GetByUserID(user.Id).Item3.TrainerId;

            var session = sessionService.GetById(Id);
            if (!session.Item1 || session.Item2.TrainerId != TrainerId)
            {
                return NotFound();
            }
            var result = sessionService.Delete(Id);
            if (!result.Item1)
            {
                TempData["ErrorMessage"] = result.Item2;
            }
            return RedirectToAction("MySessions");
        }
    }
}
