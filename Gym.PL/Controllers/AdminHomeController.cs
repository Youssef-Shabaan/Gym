using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    public class AdminHomeController : Controller
    {
      
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
