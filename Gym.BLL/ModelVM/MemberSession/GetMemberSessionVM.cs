
namespace Gym.BLL.ModelVM.MemberSession
{
    public class GetMemberSessionVM
    {
        public bool? IsAttended { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public decimal? Price { get; set; }

        // From Member
        public int MemberId { get; set; }   
        public string MemberName { get; set; }

        // From Session
        public int SessionId { get; set; }
        public string SessionName { get; set; }

        // TrainerSubscription
        public int TrainerSubscriptionId { get; set; }
        public string TrainerName { get; set;}

        // From Payment 
        public int PaymentId { get; set; }  
        public string PaymentStatus { get; set; }
    }
}
