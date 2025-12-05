
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym.DAL.Entities
{
    public class MemberPlan
    {
        public MemberPlan() { }

        public MemberPlan(int memberId, int planId, DateTime joinDate, bool isActive) 
        {
            MemberId = memberId;
            PlanId = planId;
            JoinDate = joinDate;
            IsActive = isActive;
        }

        [Key]
        public int Id { get;  set; }

        [ForeignKey("Member")]
        public int MemberId { get;  set; }
        public Member Member { get; set; }

        [ForeignKey("Plan")]
        public int PlanId { get;  set; }
        public Plan Plan { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public decimal? Price { get; set; }
        public DateTime JoinDate { get;  set; }
        public DateTime? ExpireDate { get;  set; }
        public bool IsActive { get;  set; }
        public void Active() { 
            this.IsActive = true;
        }
        public void DeActive() { 
            this.IsActive = false;
        }
        public bool Update(MemberPlan memberPlan) { 
            this.JoinDate = memberPlan.JoinDate;
            this.ExpireDate = memberPlan.ExpireDate;
            return true;
        }
    }
}
