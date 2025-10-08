
using Gym.DAL.Entities;

namespace Gym.DAL.Repo.Abstraction
{
    public interface IUserRepo
    {
        List<User> GetAll();
        User GetById(string id);
        bool Create(User newUser);
        bool Update(User newUser);
        bool Delete(string id);
    }
}
