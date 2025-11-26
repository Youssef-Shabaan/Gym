
namespace Gym.BLL.ModelVM.MemberPlan
{
    public class GetMemberPlanVM
    {
        public DateTime JoinDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool IsActive { get; set; }

        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberPhone { get; set; } 
        public string MemberImage { get; set; }

        public int PlanId { get; set; }
        public string PlanName { get; set; }

    }
}
