
using Gym.BLL.ModelVM.Session;

namespace Gym.BLL.ModelVM.Plan
{
    public class GetPlanVM
    {
        public string Name { get;  set; }
        public string Description { get;  set; }
        public DateTime StartDate { get;  set; }
        public DateTime EndDate { get;  set; }
        public decimal Price { get;  set; }
        public string TrainerName { get; set; }
    }
}
