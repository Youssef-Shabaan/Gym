using Gym.BLL.ModelVM.Admin;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.SymbolStore;

namespace Gym.PL.Controllers
{
    public class AdminHomeController : Controller
    {
        private readonly ISessionService sessionService;
        private readonly IMemberService memberService;
        private readonly IPlanService planService;
        private readonly ITrainerService trainerService;

        public AdminHomeController(ISessionService sessionService,
            IMemberService memberService,
            IPlanService planService,
            ITrainerService trainerService
        )
        {
            this.sessionService = sessionService;
            this.memberService = memberService;
            this.planService = planService;
            this.trainerService = trainerService;
        }
        public IActionResult Index()
        {
            SystemVM system = new SystemVM();
            system.members = memberService.MembersCount();
            system.trainers = trainerService.TrainersCount();
            system.sessions = sessionService.SessionsCount();
            system.plans = planService.PlansCount();    
            
            return View(system);
        }
    }
}
