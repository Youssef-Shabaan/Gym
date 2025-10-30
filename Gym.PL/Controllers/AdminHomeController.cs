using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    public class AdminHomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IAdminService adminService;
        public AdminHomeController(UserManager<User> userManager, IAdminService adminService)
        {
            _userManager = userManager;
            this.adminService = adminService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var admin = adminService.GetByUserID(user.Id);
            ViewBag.AdminName = admin.Item3.Name;
            ViewBag.AdminImage = admin.Item3.Image;
            return View();
        }
    }
}
