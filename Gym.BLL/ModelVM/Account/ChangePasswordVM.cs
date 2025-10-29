using System.ComponentModel.DataAnnotations;

namespace Gym.BLL.ModelVM.Account
{
    public class ChangePasswordVM
    {
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Password does not match.")]
        public string ConfirmPassword { get; set; }

    }
}
