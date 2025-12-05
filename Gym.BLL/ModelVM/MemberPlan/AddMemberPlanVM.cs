

namespace Gym.BLL.ModelVM.MemberPlan
{
    public class AddMemberPlanVM
    {
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public decimal? Price { get; set; }

        public int MemberId { get; set; }
        public int PlanId { get; set; }
    }
}
