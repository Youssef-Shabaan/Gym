
namespace Gym.BLL.ModelVM.Attendance
{
    public class CreateAttendanceVM
    {
        public int MemberId { get; set; }
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; } = true;
    }
}
