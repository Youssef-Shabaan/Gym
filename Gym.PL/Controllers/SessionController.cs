using Gym.BLL.ModelVM.Session;
using Gym.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly string paypalClientId = "";
        public SessionController(ISessionService sessionService, IConfiguration configuration)
        {
            _sessionService = sessionService;
            paypalClientId = configuration["PayPalSettings:ClientId"];
        }
        public IActionResult Index()
        {
            var sessions = _sessionService.GetUpcomingSessions();
            if (!sessions.Item1)
            {
                ViewBag.ErrorMessage = sessions.Item2;
                return View(new List<GetSessionVM>());
            }
            return View(sessions.Item3);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var session = _sessionService.GetById(id).Item2;
            if (session == null)
            {
                return NotFound();
            }
            ViewBag.PayPalClientId = paypalClientId;
            ViewBag.TotalAmount = session.Price.ToString(System.Globalization.CultureInfo.InvariantCulture);
            return View(session);
        }
        public IActionResult PastSession()
        {
            var sessions = _sessionService.GetPastSessions();
            if (!sessions.Item1)
            {
                ViewBag.ErrorMessage = sessions.Item2;
                return View(new List<GetSessionVM>());
            }
            return View(sessions.Item3);
        }
        public IActionResult OnGoingSession()
        {
            var sessions = _sessionService.GetOnGoingSessions();
            if (!sessions.Item1)
            {
                ViewBag.ErrorMessage = sessions.Item2;
                return View(new List<GetSessionVM>());
            }
            return View(sessions.Item3);
        }
    }
}
