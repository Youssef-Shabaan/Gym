using Gym.BLL.ModelVM.Session;
using Gym.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public IActionResult Index()
        {
            var sessions = _sessionService.GetUpcomingSessions();
            if(!sessions.Item1)
            {
                ViewBag.ErrorMessage = sessions.Item2;
                return View(new List<GetSessionVM>());
            }
            return View(sessions.Item3);
        }
        public IActionResult PastSession()
        {
            var sessions = _sessionService.GetPastSessions();
            if(!sessions.Item1)
            {
                ViewBag.ErrorMessage = sessions.Item2;
                return View(new List<GetSessionVM>());
            }
            return View(sessions.Item3);
        }
        public IActionResult OnGoingSession()
        {
            var sessions = _sessionService.GetOnGoingSessions();
            if(!sessions.Item1)
            {
                ViewBag.ErrorMessage = sessions.Item2;
                return View(new List<GetSessionVM>());
            }
            return View(sessions.Item3);
        }
    }
}
