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
            var sessions = _sessionService.GetAll();
            return View(sessions.Item3);
        }
    }
}
