
using Gym.DAL.Entities;

namespace Gym.DAL.Repo.Abstraction
{
    public interface IMemberRepo
    {
        List<Member> GetAll();
        Member GetById(int id);
        Member GetByUserId(string id);

        bool ChangePhoto(Member MemberImagePath);

		bool Create(Member newMember);
        bool Update(Member newMember, string phonNumber);
        bool Delete(int id);

        void DeletePhoto(int id);
    }
}
