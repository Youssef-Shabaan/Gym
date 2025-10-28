
using Gym.DAL.Entities;

namespace Gym.DAL.Repo.Abstraction
{
    public interface IAdminRepo
    {
        List<Admin> GetAll();
        Admin GetById(int id);
        Admin GetByUserId(string id);

		bool Create(Admin newAdmin);
        bool Update(Admin newAdmin);
        bool Delete(int id);
    }
}
