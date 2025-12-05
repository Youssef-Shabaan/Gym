using Gym.BLL.ModelVM.Admin;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    [Authorize(Roles = "Trainer")]
    public class TrainerHomeController : Controller
    {
        private readonly ISessionService sessionService;
        private readonly IMemberService memberService;
        private readonly IPlanService planService;
        private readonly ITrainerService trainerService;
        private readonly UserManager<User> userManager;
        public TrainerHomeController(ISessionService sessionService,
            IMemberService memberService,
            IPlanService planService,
            ITrainerService trainerService, UserManager<User> userManager
        )
        {
            this.sessionService = sessionService;
            this.memberService = memberService;
            this.planService = planService;
            this.trainerService = trainerService;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var TrainerId = trainerService.GetByUserID(user.Id).Item3.TrainerId;
            SystemVM system = new SystemVM();
            system.members = memberService.MembersCount();
            system.trainers = trainerService.TrainersCount();
            system.sessions = sessionService.CountSessionForTrainer(TrainerId);
            system.plans = planService.CountPlanForTrainer(TrainerId);

            return View(system);
        }
    }
}
