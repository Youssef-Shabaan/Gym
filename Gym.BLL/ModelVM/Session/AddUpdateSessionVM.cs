
using System.ComponentModel.DataAnnotations;

namespace Gym.BLL.ModelVM.Session
{
    public class AddUpdateSessionVM
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get;  set; }

        public string? Description { get;  set; }

        [Required(ErrorMessage = "Start time is required")]
        public DateTime StartTime { get;  set; }

        [Required(ErrorMessage = "End time is required")]
        public DateTime EndTime { get;  set; }
    }
}
