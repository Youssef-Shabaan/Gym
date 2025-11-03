using Gym.BLL.ModelVM.Account;
using Gym.BLL.ModelVM.Member;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Gym.DAL.Enums;
using Gym.DAL.Repo.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMemberService memberService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailService emailService;

        public AccountController(IMemberService memberService,
            UserManager<User> userManager
            , SignInManager<User> signInManager
            , IEmailService emailService
            )
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
        public async Task<IActionResult> Register(AddMemberVM newMember)
        {
            if (ModelState.IsValid)
            {
                var result = await memberService.Create(newMember);
                if (result.Item1)
                    return RedirectToAction("Login", "Account");
                else
                    ModelState.AddModelError(string.Empty, result.Item2);
            }
            return View(newMember);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(login.UserName);
                if (user != null)
                {
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


        // ---------------------------This for forgot Password ------------------
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
            try
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

                var restLink = Url.Action("ChangePassword", "Account", new { email = model.Email, token = resultToken }, Request.Scheme);

                var subject = "Reset Password";
                var body = $"Please reset your password by clicking here <a href='{restLink}'>Reset Password</a>";

                await emailService.SendEmailAsync(model.Email, subject, body);

                return RedirectToAction("EmailSent", "Account");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Somthing went error in sending email operation";
                return View(model);
            }
        }

        // Sign in with google

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
                return RedirectToAction(nameof(Login));

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            var findEmail = await userManager.FindByEmailAsync(email);
            if (findEmail != null)
            {
                await signInManager.SignInAsync(findEmail, false);
                return RedirectToAction("Index", "Home");
            }

            var user = new User { UserName = email, Email = email };
            var createResult = await userManager.CreateAsync(user);
            if (createResult.Succeeded)
            {
                await userManager.AddLoginAsync(user, info);
                await userManager.AddToRoleAsync(user, "Member");
                await signInManager.SignInAsync(user, false);

                var name = info.Principal.FindFirstValue(ClaimTypes.Name) ?? "New Member";
                var image = info.Principal.FindFirstValue("picture");
                var gender = Gender.Male;
                string? address = null;
                var age = 0;

                //var member = new Member(name, gender, image, age, address, user.Id);
                //memberRepo.Create(member);
                var AddMember = await memberService.CreateMemberForEmail(name, gender, image, age, address, user.Id);

                if(!AddMember.Item1)
                {
                    TempData["AddMemberForEmail"] = AddMember.Item2;
                    await signInManager.SignOutAsync();
                    return RedirectToAction(nameof(Register));
                }

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangeAccountPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeAccountPassword(ChangeAccountPasswordVM model)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                    return RedirectToAction("Login");

                var checkOld = await userManager.CheckPasswordAsync(user, model.OldPassword);
                if (!checkOld)
                {
                    ModelState.AddModelError("", "Old password is incorrect");
                    return View(model);
                }

                var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    TempData["Success"] = "Password changed successfully!";
                    return RedirectToAction("Index", "Profile");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}
