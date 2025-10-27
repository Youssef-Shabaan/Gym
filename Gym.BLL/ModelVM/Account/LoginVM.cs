
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gym.BLL.ModelVM.Account
{
    public class LoginVM
    {
        [DisplayName("User Name")]
        [Required(ErrorMessage ="Enter your username")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Enter your password")]
        public string Password { get; set; }
        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}
