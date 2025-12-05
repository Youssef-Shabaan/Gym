
namespace Gym.BLL.ModelVM.MemberPlan
{
    public class GetMemberPlanVM
    {
        public DateTime JoinDate { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }

        public string TrainerName { get; set; }
        public string TrainerPhone { get; set; }    

        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public string Description { get; set; }

    }
}
