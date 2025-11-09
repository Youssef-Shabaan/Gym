
using System.ComponentModel.DataAnnotations;

namespace Gym.BLL.ModelVM.Session
{
    public class UpdateSessionVM
    {
        int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        public DateTime EndTime { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
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
