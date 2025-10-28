using Gym.BLL.ModelVM.Account;
using Gym.BLL.ModelVM.Member;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMemberService memberService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public AccountController(IMemberService memberService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.memberService = memberService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> Register(AddMemberVM newMember) {
            if (ModelState.IsValid) {
                var result = await memberService.Create(newMember);
                if(result.Item1)
                    return RedirectToAction("Login", "Account");
                else
                    ModelState.AddModelError(string.Empty, result.Item2);
            }
            return View(newMember);
        }
        [HttpGet]
        public IActionResult Login() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid) {
                var user = await userManager.FindByNameAsync(login.UserName);
                if (user != null) {
                    bool found = await userManager.CheckPasswordAsync(user, login.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(user, login.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                } 
            }
            ModelState.AddModelError("", "username or password error");
            return View(login);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
