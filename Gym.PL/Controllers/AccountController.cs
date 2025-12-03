using Gym.BLL.ModelVM.Account;
using Gym.BLL.ModelVM.Member;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Gym.DAL.Enums;
using Gym.DAL.Repo.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Gym.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMemberService memberService;
        private readonly ITrainerService trainerService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailService emailService;

        public AccountController(IMemberService memberService,
            UserManager<User> userManager
            , SignInManager<User> signInManager
            , IEmailService emailService
            , ITrainerService trainerService

            )
        {
            this.memberService = memberService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailService = emailService;
            this.trainerService = trainerService;
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
                var existingUser = await userManager.FindByEmailAsync(newMember.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "This email is already registered.");
                    return View();
                }
                var result = await memberService.Create(newMember, false);
                if (!result.Item1)
                    ModelState.AddModelError(string.Empty, result.Item2);


                var user = await userManager.FindByEmailAsync(newMember.Email);

                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, token = token },
                            Request.Scheme
                        );
                var subject = "Confirm Your Email";
                var body = $@"
                        <h3>Welcome to FitHub System 💪</h3>
                        <p>Please confirm your email by clicking the link below:</p>
                        <a href='{confirmationLink}'>Confirm Email</a>";

                try
                {
                    await emailService.SendEmailAsync(user.Email, subject, body);
                }
                catch (Exception ex)
                {
                    await userManager.DeleteAsync(user);
                    return RedirectToAction("ErrorInConfirmeEmail");
                }

                return RedirectToAction("EmailSent");
            }
            return View(newMember);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
                return BadRequest("Invalid confirmation link");

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            try
            {
                var result = await userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                    return View("EmailConfirmed");
            }
            catch
            {
                await userManager.DeleteAsync(user);
                return View("ErrorInConfirmeEmail");
            }

            await userManager.DeleteAsync(user);
            return View("ErrorInConfirmeEmail");
        }

        [HttpGet]
        public IActionResult EmailConfirmed()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ErrorInConfirmeEmail()
        {
            return View();
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
                    if (!user.EmailConfirmed)
                    {
                        ModelState.AddModelError("", "Please confirm your email first.");
                        return View(login);
                    }

                    bool found = await userManager.CheckPasswordAsync(user, login.Password);
                    if (found)
                    {
                        var member = memberService.GetByUserID(user.Id);
                        var claims = new List<Claim>();
                        if (member.Item3 != null)
                        {
                            claims.Add(new Claim("MemberId", member.Item3.MemberId.ToString()));
                        }

                        await signInManager.SignInWithClaimsAsync(user, login.RememberMe, claims);

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
                    await signInManager.SignOutAsync();
                    await userManager.DeleteAsync(user);
                    TempData["AddMemberForEmail"] = AddMember.Item2;
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
