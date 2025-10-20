
using Gym.BLL.ModelVM.MemberShip;
using Gym.DAL.Entities;

namespace Gym.BLL.Service.Abstraction
{
    public interface IMemberShipService
    {
        (bool, string, List<GetAllMemberShipVM>) GetAll();
        (bool, string, GetMemberShipVM) GetById(int id);
        bool Create(MemberShip newMemberShip);
        bool Update(MemberShip newMemberShip);
        bool Delete(int id);
    }
}
