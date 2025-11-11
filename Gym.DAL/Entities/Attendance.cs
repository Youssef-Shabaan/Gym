
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.DAL.Entities
{
    public class Attendance
    {
        public Attendance() { }
        public Attendance(DateTime date, bool isPresent, int memberId, int sessionId)
        {
            Date = date;
            IsPresent = isPresent;
            MemberId = memberId;
            SessionId = sessionId;
        }
        [Key]
        public int Id { get; private set; }
        public DateTime Date { get; private set; }
        public bool IsPresent { get; private set; }

        [ForeignKey("member")]
        public int MemberId { get; private set; }
        public Member member { get; private set; }

        [ForeignKey("Session")]
        public int SessionId { get; private set; }
        public Session Session { get; private set; }
        public bool EditAttendance(Attendance attendance)
        {
            if (attendance == null) return false;
            Date = attendance.Date;
            MemberId = attendance.MemberId;
            SessionId = attendance.SessionId;
            IsPresent = attendance.IsPresent;
            return true;
        }
    }
}
