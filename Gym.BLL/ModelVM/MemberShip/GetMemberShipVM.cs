using Gym.BLL.ModelVM.Member;
using Gym.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BLL.ModelVM.MemberShip
{
    public class GetMemberShipVM
    {
        public MemberShipType MemberShipType { get; set; }
        public double Price { get; set; }
        public GetMemberVM? getMemberVMs { get; set; }
    }
}
