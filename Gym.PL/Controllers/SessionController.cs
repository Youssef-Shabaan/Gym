using Gym.BLL.ModelVM.MemberSession;
using Gym.BLL.ModelVM.Session;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IMemberService _memberSerice;
        private readonly UserManager<User> _userManager;
        private readonly IMemberSessionService _memberSessionService;
        private readonly string paypalClientId = "";
        public SessionController(IMemberService _memberSerice, UserManager<User> _userManager, ISessionService sessionService, IConfiguration configuration, IMemberSessionService memberSessionService)
        {
            this._memberSerice = _memberSerice;
            this._userManager = _userManager;
            _sessionService = sessionService;
            _memberSessionService = memberSessionService;
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
        [Authorize]
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
        //public IActionResult PastSession()
        //{
        //    var sessions = _sessionService.GetPastSessions();
        //    if (!sessions.Item1)
        //    {
        //        ViewBag.ErrorMessage = sessions.Item2;
        //        return View(new List<GetSessionVM>());
        //    }
        //    return View(sessions.Item3);
        //}
        //public IActionResult OnGoingSession()
        //{
        //    var sessions = _sessionService.GetOnGoingSessions();
        //    if (!sessions.Item1)
        //    {
        //        ViewBag.ErrorMessage = sessions.Item2;
        //        return View(new List<GetSessionVM>());
        //    }
        //    return View(sessions.Item3);
        //}

        [HttpGet]
        public IActionResult MySessions()
        {
            var userId = _userManager.GetUserId(User);
            var member = _memberSerice.GetByUserID(userId).Item3;
            var memberId = member.MemberId;
            var mySessions = _memberSessionService.GetByMemberId(memberId);
            if (!mySessions.Item1)
            {
                return View(new List<GetMemberSessionVM>());
            }
            return View(mySessions.Item3);
        }
    }
}
