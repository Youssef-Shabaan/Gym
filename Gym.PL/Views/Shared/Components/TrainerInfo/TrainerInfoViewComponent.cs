using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class TrainerInfoViewComponent : ViewComponent
{
    private readonly UserManager<User> _userManager;
    private readonly ITrainerService _trainerService;

    public TrainerInfoViewComponent(UserManager<User> userManager, ITrainerService trainerService)
    {
        _userManager = userManager;
        _trainerService = trainerService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // Get logged-in user
        var user = await _userManager.GetUserAsync(UserClaimsPrincipal);
        if (user == null)
            return Content(""); // no logged in user

        // Get trainer info by user ID
        var trainer = _trainerService.GetByUserID(user.Id).Item3;

        ViewBag.TrainerName = trainer?.Name ?? "Trainer";
        ViewBag.TrainerImage = trainer?.Image ?? "default.png";

        return View();
    }
}
