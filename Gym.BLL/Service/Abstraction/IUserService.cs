
using Gym.BLL.ModelVM.User;

namespace Gym.BLL.Service.Abstraction
{
    public interface IUserService
    {
        (bool, string, List<GetUserVM>) GetAllUsers();
        (bool, string, GetUserVM) GetUserById(string id);
    }
}
