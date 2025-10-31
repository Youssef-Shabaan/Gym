using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AdminInfoViewComponent : ViewComponent
{
    private readonly UserManager<User> _userManager;
    private readonly IAdminService _adminService;

    public AdminInfoViewComponent(UserManager<User> userManager, IAdminService adminService)
    {
        _userManager = userManager;
        _adminService = adminService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userManager.GetUserAsync(UserClaimsPrincipal);
        if (user == null)
            return Content(""); // no logged in user

        var admin = _adminService.GetByUserID(user.Id);

        ViewBag.AdminName = admin.Item3?.Name ?? "Admin";
        ViewBag.AdminImage = admin.Item3?.Image ?? "default.png";

        return View();
    }
}
