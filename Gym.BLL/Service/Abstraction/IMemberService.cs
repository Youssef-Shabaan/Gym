
using Gym.BLL.ModelVM.Member;
using Gym.DAL.Enums;

namespace Gym.BLL.Service.Abstraction
{
    public interface IMemberService
    {
        (bool, string, List<GetMemberVM>) GetAll();
        (bool, string, GetMemberVM) GetByID(int id);
        (bool, string, GetMemberVM) GetByUserID(string id);

		Task<(bool, string)> Delete(int id);
        (bool, string) Update(int id, EditMemberVM curr);
        Task<(bool, string)> Create(AddMemberVM newmember);

        Task<(bool, string)> CreateMemberForEmail(string name, Gender gender, string image, int age, string address, string userId);
    }
}