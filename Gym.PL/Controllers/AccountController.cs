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
        private readonly IEmailService emailService;
        public AccountController(IMemberService memberService, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager
            ,IEmailService emailService)
        {
            this.memberService = memberService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailService = emailService;
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


        // ----------------------------------------
        [HttpGet]
        public IActionResult ChangePassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }

            var model = new ChangePasswordVM { Email = email, Token = token };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found!");
                return View(model);
            }

            var resetResult = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!resetResult.Succeeded)
            {
                foreach (var error in resetResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        // ---------------------------------------------------

        [HttpGet]
        public IActionResult EmailSent()
        {
            return View();
        }

        // ---------------------------------------------
        [HttpGet]
        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyEmail(VerifyEmailVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found!");
                return View(model);
            }

            var resultToken = await userManager.GeneratePasswordResetTokenAsync(user);

            var restLink = Url.Action("ChangePassword", "Account", new { emailService = model.Email, token = resultToken }, Request.Scheme);

            var subject = "Reset Password";
            var body = $"Please reset your password by clicking here <a href='{restLink}'>Reset Password</a>";

            await emailService.SendEmailAsync(model.Email, subject, body);

            return RedirectToAction("EmailSent", "Account");

        }



    }
}
