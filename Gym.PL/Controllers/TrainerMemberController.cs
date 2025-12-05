using Gym.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    [Authorize(Roles = "Trainer")]
    public class TrainerMemberController : Controller
    {
        private readonly IMemberService _memberService;
        public TrainerMemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public IActionResult Index()
        {
            var members = _memberService.GetAll();
            TempData["ErrorMessage"] = members.Item2;
            return View(members.Item3);
        }
    }
}
