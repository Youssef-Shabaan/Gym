
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gym.BLL.ModelVM.Account
{
    public class ChangeAccountPasswordVM
    {
        [Required(ErrorMessage ="Old password is required")]
        [DisplayName("Old Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage ="New password is required")]
        [DisplayName("New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Password does not match")]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
