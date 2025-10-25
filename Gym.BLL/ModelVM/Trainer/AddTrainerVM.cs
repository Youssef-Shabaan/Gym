
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Gym.BLL.ModelVM.Trainer
{
    public class AddTrainerVM
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name must contain at least two chars")]
        public string Name { get; set; }

        [Range(18, 40, ErrorMessage = "Age must be between 18 and 40")]
        public int Age { get; set; }

        public string Info { get; set; }
        public string Address { get; set; }
        
        public IFormFile Image { get; set; }

        public int Capacity { get; set; }
    }
}
