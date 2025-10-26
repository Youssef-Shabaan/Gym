
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;

namespace Gym.DAL.Repo.Implementation
{
    public class AdminRepo : IAdminRepo
    {
        private readonly GymDbContext _context;
        public AdminRepo(GymDbContext context)
        {
            _context = context;
        }

        public bool Create(Admin newAdmin)
        {
            try {
                _context.admins.Add(newAdmin);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public bool Delete(int id)
        {
            try
            {
                var result = _context.admins.FirstOrDefault(a => a.AdminId == id);
                _context.admins.Remove(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public List<Admin> GetAll()
        {
            try {
                var result = _context.admins.ToList();
                return result;
            }
            catch (Exception ex) { return null; }
            
        }

        public Admin GetById(int id)
        {
            try
            {
                var result = _context.admins.FirstOrDefault(a => a.AdminId == id);
                return result;
            }
            catch (Exception ex) { return null; }
        }

        public bool Update(Admin newAdmin)
        {
            try {
                var result = _context.admins.FirstOrDefault(a => a.AdminId == newAdmin.AdminId);
                result.update(newAdmin);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) { return false; }
        }
    }
}
