using System.ComponentModel.DataAnnotations;

namespace Gym.BLL.ModelVM.Account
{
    public class VerifyEmailVM
    {
        [EmailAddress]
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }

    }
}
