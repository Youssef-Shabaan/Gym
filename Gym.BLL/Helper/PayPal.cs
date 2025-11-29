
using System.ComponentModel.DataAnnotations;

namespace Gym.BLL.Helper
{
    public class PayPal
    {
        [Required]
        public string PayPalClientId { get; set; } = "";
        [Required]
        public string PayPalSecret { get; set; } = "";
        [Required]
        public string PayPalUrl { get; set; } = "";
    }
}
