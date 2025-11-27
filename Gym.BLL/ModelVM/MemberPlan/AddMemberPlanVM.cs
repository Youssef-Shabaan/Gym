
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.BLL.ModelVM.MemberPlan
{
    public class AddMemberPlanVM
    {
        public int MemberId { get;  set; }
        public int PlanId { get;  set; }
        public DateTime JoinDate { get;  set; }
        public DateTime? ExpireDate { get;  set; }
        public bool IsActive { get;  set; } = true;
    }
}
