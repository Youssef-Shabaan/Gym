
using Gym.BLL.ModelVM.Trainer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Gym.BLL.ModelVM.Session
{
    public class AddSessionVM : IValidatableObject
    {
        public int TrainerId { get; set; }

        [ValidateNever]
        public IEnumerable<GetTrainerVM> Trainers { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get;  set; }

        public string? Description { get;  set; }

        [Required(ErrorMessage = "Start time is required")]
        public DateTime StartTime { get;  set; } = DateTime.Now;

        [Required(ErrorMessage = "End time is required")]
        public DateTime EndTime { get; set; } = DateTime.Now;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(StartTime <  DateTime.Now)
            {
                yield return new ValidationResult(
                    "Start time must be grater than now date",
                    new[] { nameof(StartTime) }
                );
            }
            if (EndTime <= StartTime)
            {
                yield return new ValidationResult(
                    "End time must be greater than start time",
                    new[] { nameof(EndTime) }
                );
            }
        }

    }
}
