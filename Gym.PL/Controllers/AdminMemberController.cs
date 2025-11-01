using Gym.BLL.ModelVM.Member;
using Gym.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminMemberController : Controller
    {
        private readonly IMemberService _memberService;
        public AdminMemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public IActionResult Index()
        {
            var members = _memberService.GetAll();
            TempData["ErrorMessage"] = members.Item2;
            return View(members.Item3);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddMemberVM addMemberVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _memberService.Create(addMemberVM);
                if (result.Item1)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, result.Item2);
            }
            return View(addMemberVM);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _memberService.Delete(id);
            if (result.Item1)
                return RedirectToAction("Index");
            TempData["ErrorMessage"] = result.Item2;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var member = _memberService.GetByID(id);

            if (!member.Item1)
            {
                return NotFound();
            }
            
            var editMember = new EditMemberVM()
            {
                UserId = member.Item3.UserId,
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
            if (ModelState.IsValid)
            {
                var result = _memberService.Update(editMember);
                if (!result.Item1)
                {
                    ViewBag.ErrorMessage = result.Item2;
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View(editMember);
        }
    }
}