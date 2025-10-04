
using Gym.DAL.Entities;

namespace Gym.DAL.Repo.Abstraction
{
    public interface IMemberRepo
    {
        List<Member> GetAll();
        Member GetById(int id);
        bool Create(Member newMember);
        bool Update(Member newMember);
        bool Delete(int id);
    }
}
