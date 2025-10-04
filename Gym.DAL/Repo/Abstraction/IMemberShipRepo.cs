
using Gym.DAL.Entities;

namespace Gym.DAL.Repo.Abstraction
{
    public interface IMemberShipRepo
    {
        List<MemberShip> GetAll();
        MemberShip GetById(int id);
        bool Create(MemberShip newMemberShip);
        bool Update(MemberShip newMemberShip);
        bool Delete(int id);
    }
}