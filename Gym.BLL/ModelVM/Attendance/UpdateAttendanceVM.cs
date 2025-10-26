using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BLL.ModelVM.Attendance
{
    public class UpdateAttendanceVM
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
    }
}
