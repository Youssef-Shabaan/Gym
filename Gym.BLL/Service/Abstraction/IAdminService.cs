
using Gym.BLL.ModelVM.Admin;
using Gym.BLL.ModelVM.Member;

namespace Gym.BLL.Service.Abstraction
{
    public interface IAdminService
    {
        (bool, string, List<GetAdminVM>) GetAll();
        (bool, string, GetAdminVM) GetByID(int id);
        (bool, string, GetAdminVM) GetByUserID(string id);

		Task<(bool, string)> Delete(int id);
        (bool, string) Update(int id, EditAdminVM curr);
        Task<(bool, string)> Create(AddAdminVM newmember);
    }
}
