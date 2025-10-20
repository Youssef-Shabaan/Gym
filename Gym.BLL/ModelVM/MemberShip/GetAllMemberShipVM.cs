
using Gym.BLL.ModelVM.Member;
using Gym.DAL.Enums;

namespace Gym.BLL.ModelVM.MemberShip
{
    public class GetAllMemberShipVM
    {
        public MemberShipType MemberShipType { get;  set; }
        public double Price { get;  set; }
        public IEnumerable<GetMemberVM> getMemberVMs { get; set; }
    }
}
