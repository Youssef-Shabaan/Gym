
namespace Gym.BLL.ModelVM.Plan
{
    public class AddPlanVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        // trainerId ya hussien 3lshan el realation
        public int TrainerId { get; set; }  

    }
}
