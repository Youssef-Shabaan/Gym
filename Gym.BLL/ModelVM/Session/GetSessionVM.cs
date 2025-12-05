

using System.ComponentModel;

namespace Gym.BLL.ModelVM.Session
{
    public class GetSessionVM
    {
        public int Id { get; set; }
        public string Name { get;  set; }
        public string? Description { get;  set; }
        public DateTime StartTime { get;  set; }
        public DateTime EndTime { get;  set; }
        public string TrainerName { get; set; }
        public string TrainerPhone { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public int Capactiy { get; set; }
        public int? PlanId { get; set; }
        public int Booked { get; set; }
    }
}
