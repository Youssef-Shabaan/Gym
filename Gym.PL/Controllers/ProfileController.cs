using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
	[Authorize]
	public class ProfileController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly IAdminService _adminService;
		private readonly ITrainerService _trainerService;
		private readonly IMemberService _memberService;
		public ProfileController(UserManager<User> _userManager, IAdminService _adminService, ITrainerService _trainerService, IMemberService _memberService)
		{
			this._userManager = _userManager;
			this._adminService = _adminService;
			this._trainerService = _trainerService;
			this._memberService = _memberService;
		}
		public async Task<IActionResult> Index()
		{
			var currentUser = await _userManager.GetUserAsync(User);

			if (User.IsInRole("Admin"))
			{
				var admin =  _adminService.GetByUserID(currentUser.Id);
				return View("AdminProfile", admin.Item3);
			}
			else if (User.IsInRole("Trainer"))
			{
				var trainer =  _trainerService.GetByUserID(currentUser.Id);
				return View("TrainerProfile", trainer.Item3);
			}
			else if (User.IsInRole("Member"))
			{
				var member =  _memberService.GetByUserID(currentUser.Id);
				return View("MemberProfile", member.Item3);
			}

			return RedirectToAction("Login", "Account");
		}

	}
}
