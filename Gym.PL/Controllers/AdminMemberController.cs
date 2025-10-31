using Gym.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminMemberController : Controller
    {
        private readonly IMemberService _memberService;
        public AdminMemberController(IMemberService memberService)
        {
			_memberService = memberService;
		}
        public IActionResult Index()
        {
            var members = _memberService.GetAll();
            ViewBag.ErrorMessage = members.Item2;
            return View(members.Item3);
        }
    }
}
