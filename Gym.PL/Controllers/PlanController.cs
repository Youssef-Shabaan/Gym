using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    public class PlanController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
