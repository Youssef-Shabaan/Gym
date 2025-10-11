
using System.ComponentModel.DataAnnotations;

namespace Gym.BLL.ModelVM.Session
{
    public class AddUpdateSessionVM
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get;  set; }

        public string? Description { get;  set; }

        [Required(ErrorMessage = "Schedule Time is required")]
        public DateTime ScheduleTime { get;  set; }

        [Required(ErrorMessage = "Capacity is required")]
        public int Capacity { get;  set; }
    }
}
