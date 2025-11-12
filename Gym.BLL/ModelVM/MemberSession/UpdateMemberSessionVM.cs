using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BLL.ModelVM.MemberSession
{
    public class UpdateMemberSessionVM
    {
        public bool? IsAttended { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public decimal? Price { get; set; }
    }
}
