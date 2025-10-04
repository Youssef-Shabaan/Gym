
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.DAL.Entities
{
    public class Attendance
    {
        [Key]
        public int id { get;private set; }
        public DateTime date { get; private set; }
        [ForeignKey("member")]
        public int memberId { get; private set; }
        public Member member { get; private set; }
        public bool isPresent { get; private set; }
        public bool EditAttendance(Attendance attendance)
        {
            if(attendance == null) return false;
            date = attendance.date;
            memberId = attendance.memberId;
            isPresent = attendance.isPresent;
            return true;
        }
    }
}
