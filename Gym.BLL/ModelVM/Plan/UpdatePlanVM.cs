
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Gym.BLL.ModelVM.Plan
{
    public class UpdatePlanVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        [DisplayName("Start Time")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End time is required")]
        [DisplayName("End Time")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int Capcity { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate < DateTime.Now)
            {
                yield return new ValidationResult(
                    "Start time must be grater than now date",
                    new[] { nameof(StartDate) }
                );
            }
            if (EndDate <= StartDate)
            {
                yield return new ValidationResult(
                    "End time must be greater than start time",
                    new[] { nameof(EndDate) }
                );
            }
        }
    }
}
