using Gym.BLL.ModelVM.MemberSession;
using Gym.BLL.ModelVM.Session;
using Gym.BLL.ModelVM.Trainer;
using Gym.BLL.Service.Abstraction;
using Gym.BLL.Service.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminSessionController : Controller
    {
        private readonly ISessionService sessionService;
        private readonly ITrainerService trainerService;
        private readonly IMemberSessionService memberSessionService;

        public AdminSessionController(ISessionService sessionService, ITrainerService trainerService, IMemberSessionService memberSessionService)
        {
            this.sessionService = sessionService;
            this.trainerService = trainerService;
            this.memberSessionService = memberSessionService;
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
        [ValidateAntiForgeryToken]
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
        [HttpGet]
        public IActionResult Edit(int Id)
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
            };
            return View(updatedSession);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateSessionVM updatedSession)
        {
            if(ModelState.IsValid)
            {
                var result = sessionService.Update(updatedSession);
                if(!result.Item1)
                {
                    ModelState.AddModelError("", result.Item2);
                    return View(updatedSession);
                }
                return RedirectToAction("Index");
            }
            return View(updatedSession);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var session = sessionService.GetById(Id);
            if (!session.Item1)
            {
                return NotFound();
            }
            var result = sessionService.Delete(Id);
            if(!result.Item1)
            {
                TempData["ErrorMessage"] = result.Item2;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetSessionsForUser(int id)
        {
            var sessions = memberSessionService.GetByMemberId(id);
            if(!sessions.Item1)
            {
                return View(new List<GetMemberSessionVM>());
            }
            return View(sessions.Item3);
        }

        [HttpGet]
        public IActionResult GetSessionsForTrainer(int id)
        {
            var sessions = trainerService.GetTrainerSessions(id);
            if(!sessions.Item1 && sessions.Item2 == "Trainer not found")
            {
                return NotFound();
            }
            if(!sessions.Item1)
            {
                return View(new List<GetSessionVM>());    
            }
            return View(sessions.Item3);
        }

        [HttpGet]
        public IActionResult GetMembersForSession(int SessionId)
        {
            var members = memberSessionService.GetMembersForSession(SessionId);
            if(!members.Item1 &&  members.Item2 == "Session not found")
            {
                return NotFound();
            }
            return View(members.Item3);
        }

        [HttpGet]
        public IActionResult DeleteMemberFromSession(int Id) 
        {
            var sessionId = memberSessionService.GetSessionId(Id);
            if (sessionId == -1)
            {
                return NotFound();
            }
            var result = memberSessionService.Delete(Id);
            if (!result.Item1)
            {
                ViewBag.ErrorMessage = result.Item2;
            }
            return RedirectToAction("GetMembersForSession", new { sessionId });
        }
    }
}
