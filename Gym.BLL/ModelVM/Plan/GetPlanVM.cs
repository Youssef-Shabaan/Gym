
using Gym.BLL.ModelVM.Session;

namespace Gym.BLL.ModelVM.Plan
{
    public class GetPlanVM
    {
        public int Id { get; set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public DateTime StartDate { get;  set; }
        public DateTime EndDate { get;  set; }
        public decimal Price { get;  set; }
        public string TrainerName { get; set; }
        public int TrainerId { get;  set; }

        public int Capcity { get; set; }
        public int Booked { get; set; }
        public string Status { get; set; }
    }
}
