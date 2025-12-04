using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.Trainer;
using Gym.BLL.Service.Abstraction;
using Gym.BLL.Service.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTrainerController : Controller
    {
        private readonly ITrainerService _trainerService;
        public AdminTrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        public IActionResult Index()
        {
            var trainers = _trainerService.GetAll();
            TempData["ErrorMessage"] = trainers.Item2;
            return View(trainers.Item3);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddTrainerVM addTrainerVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _trainerService.Create(addTrainerVM);
                if (result.Item1)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, result.Item2);
            }
            return View(addTrainerVM);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _trainerService.Delete(id);
            if (result.Item1)
                return RedirectToAction("Index");
            TempData["ErrorMessage"] = result.Item2;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var trainer = _trainerService.GetByID(id);

            if (!trainer.Item1)
            {
                return NotFound();
            }

            var editTrainer = new UpdateTrainerVM()
            {
                UserId = trainer.Item3.UserId,
                Id = id,
                Name = trainer.Item3.Name,
                Age = trainer.Item3.Age,
                Address = trainer.Item3.Address,
                PhoneNumber = trainer.Item3.PhoneNumber,
            };
            return View(editTrainer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateTrainerVM editTrainer)
        {
            if (ModelState.IsValid)
            {
                var result = _trainerService.Update(editTrainer.Id,editTrainer);
                if (!result.Item1)
                {
                    ViewBag.ErrorMessage = result.Item2;
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View(editTrainer);
        }
    }
}