
using Gym.DAL.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Gym.BLL.ModelVM.Member
{
    public class AddMemberVM
    {
        [Display(Name ="First Name")]
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name must contain at least two chars")]
        public string FisrtName { get; set; }
        [Display(Name ="Last Name")]
        //[Required(ErrorMessage = "Name is required")]
        //[MinLength(2, ErrorMessage = "Name must contain at least two chars")]
        [ValidateNever]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be 11 digits")]
        public string PhoneNumber { get; set; }

        [Range(18, 40, ErrorMessage = "Age must be between 18 and 40")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }
        public string Address { get; set; }

        public IFormFile? Image { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        //[Display(Name ="Confirm Password")]
        //[Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        [ValidateNever]
        public string confirmPassword { get; set; }
    }
}
