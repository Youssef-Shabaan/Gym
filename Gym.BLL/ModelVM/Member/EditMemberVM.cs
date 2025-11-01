
using Gym.DAL.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Gym.BLL.ModelVM.Member
{
    public class EditMemberVM
    {
        public string UserId { get; set; }
        public int Id { get; set; } 

        [MinLength(2, ErrorMessage = "Name must contain at least two chars")]
        public string? Name { get; set; }

        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60")]
        public int? Age { get; set; }

        public string? Address { get; set; }


        [Required(ErrorMessage = "Phone number is required.")]
        public string? PhoneNumber { get; set; }

    }
}
