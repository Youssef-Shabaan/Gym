
using Gym.DAL.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Gym.BLL.ModelVM.Member
{
    public class EditMemberVM
    {
        [Required]
        public int Member_Id { get; set; }

        [MinLength(2, ErrorMessage = "Name must contain at least two chars")]
        public string? Name { get; set; }

        [Range(18, 40, ErrorMessage = "Age must be between 18 and 40")]
        public int? Age { get; set; }

        public Gender? Gender { get; set; }

        public string? Address { get; set; }

        public IFormFile? Image { get; set; }

        public string? Info { get; set; }
    }
}
