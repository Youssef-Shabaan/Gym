
using Gym.BLL.ModelVM.Member;

namespace Gym.BLL.Service.Abstraction
{
    public interface IMemberService
    {
        (bool, string, List<GetMemberVM>) GetAll();
        (bool, string, GetMemberVM) GetByID(int id);
        Task<(bool, string)> Delete(int id);
        (bool, string) Update(int id, EditMemberVM curr);
        Task<(bool, string)> Create(AddMemberVM newmember);
    }
}