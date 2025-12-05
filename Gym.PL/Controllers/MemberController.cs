using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.ModifyPhotos;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym.PL.Controllers
{
    [Authorize(Roles =("Member"))]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public MemberController(
            IMemberService memberService
            ,UserManager<User> userManager
            )
        {
            this._memberService = memberService;
            this._userManager = userManager;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var member = _memberService.GetByID(id);

            if(!member.Item1)
            {
                return NotFound();
            }

            var UserId = User.Claims.FirstOrDefault(c => c.Type==ClaimTypes.NameIdentifier);
            var userId = UserId.Value;
            var isAdmin = User.IsInRole("Admin");

            if(member.Item3.UserId != userId && !isAdmin)
            {
                return Unauthorized();
            }

            var editMember = new EditMemberVM()
            {
                UserId = userId,
                Id = id,
                Name = member.Item3.Name,
                Age = member.Item3.Age,
                Address = member.Item3.Address,
                PhoneNumber = member.Item3.PhoneNumber
            };
            return View(editMember);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditMemberVM editMember)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userId = UserId.Value;
            var isAdmin = User.IsInRole("Admin");

            if (editMember.UserId != userId && !isAdmin)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                var result = _memberService.Update(editMember);
                if(!result.Item1)
                {
                    ViewBag.ErrorMessage = result.Item2;
                    return View();
                }
                return RedirectToAction("Index", "Profile");
            }
            return View(editMember);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePhoto(ChangePhotoVM changePhoto)
        {
            var result = _memberService.ChangePhoto(changePhoto);
            return RedirectToAction("Index", "Profile");
        }

        [HttpPost]
        public IActionResult DeletePhoto(int id)
        {
            _memberService.DeletePhoto(id);
            return RedirectToAction("Index", "Profile");
        }
    }
}
