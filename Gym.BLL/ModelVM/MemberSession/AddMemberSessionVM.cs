
namespace Gym.BLL.ModelVM.MemberSession
{
    public class AddMemberSessionVM
    {
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public decimal? Price { get; set; }

        public int MemberId { get; set; }
        public int SessionId { get; set; }
    }
}
