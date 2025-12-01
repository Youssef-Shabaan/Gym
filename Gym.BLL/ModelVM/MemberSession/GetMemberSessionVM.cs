
namespace Gym.BLL.ModelVM.MemberSession
{
    public class GetMemberSessionVM
    {
        public bool? IsAttended { get; set; }  //
        public DateTime BookingDate { get; set; } 

        // From Member
        public int MemberId { get; set; }   
        public string MemberName { get; set; }

        // From Session
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public string SessionDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // Trainer
        public string TrainerName { get; set;}
        public string TrainerPhone { get; set; }
    }
}
