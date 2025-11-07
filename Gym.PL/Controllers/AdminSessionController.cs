using Gym.BLL.ModelVM.Session;
using Gym.BLL.ModelVM.Trainer;
using Gym.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminSessionController : Controller
    {
        private readonly ISessionService sessionService;
        private readonly ITrainerService trainerService;

        public AdminSessionController(ISessionService sessionService, ITrainerService trainerService)
        {
            this.sessionService = sessionService;
            this.trainerService = trainerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sessions = sessionService.GetAll();
            if(!sessions.Item1)
            {
                TempData["ErrorMessage"] = sessions.Item2;
            }
            return View(sessions.Item3);
        }


        [HttpGet]
        public IActionResult Add()
        {
            AddSessionVM addSession = new AddSessionVM();
            var trainers = trainerService.GetAll();
            if(!trainers.Item1)
            {
                ViewBag.ErrorMessage = trainers.Item2;
                return View(new AddSessionVM());
            }
            addSession.Trainers = trainers.Item3;
            return View(addSession);
        }
        [HttpPost]
        public IActionResult Add(AddSessionVM addSession)
        {
            addSession.Trainers = trainerService.GetAll().Item3;
            if(ModelState.IsValid)
            {
                var result = sessionService.AddSession(addSession);
                if(!result.Item1)
                {
                    ViewBag.ErrorMessage = result.Item2;
                    return View(addSession);
                }
                return RedirectToAction("Index");
            }
            return View(addSession);
        }
    }
}
