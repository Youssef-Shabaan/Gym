
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym.DAL.Entities
{
    public class MemberPlan
    {
        [Key]
        public int Id { get; private set; }

        [ForeignKey("Member")]
        public int MemberId { get; private set; }
        public Member Member { get; set; }

        [ForeignKey("Plan")]
        public int PlanId { get; private set; }
        public Plan Plan { get; set; }

        public DateTime JoinDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool IsActive { get; private set; }
        public void Active() { 
            this.IsActive = true;
        }public void DeActive() { 
            this.IsActive = false;
        }
        public bool Update(MemberPlan memberPlan) { 
            this.JoinDate = memberPlan.JoinDate;
            this.ExpireDate = memberPlan.ExpireDate;
            return true;
        }
    }
}
