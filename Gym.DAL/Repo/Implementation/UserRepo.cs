
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;

namespace Gym.DAL.Repo.Implementation
{
    public class UserRepo : IUserRepo
    {
        private readonly GymDbContext DB;
        public UserRepo(GymDbContext DB)
        {
            this.DB = DB;
        }
        public bool Create(User newUser)
        {
            try
            {
                if (newUser == null) return false;
                DB.Users.Add(newUser);
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Delete(string id)
        {
            try
            {
                var result = DB.Users.Where(m => m.Id == id).FirstOrDefault();
                if (result == null) return false;
                DB.Users.Remove(result);
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public List<User> GetAll()
        {
            var result = DB.Users.ToList();
            return result;
        }

        public User GetById(string id)
        {
            var result = DB.Users.Where(m => m.Id == id).FirstOrDefault();
            return result;
        }

        public bool Update(User newUser)
        {
            try
            {
                var result = DB.users.Where(m => m.Id == newUser.Id).FirstOrDefault();
                if (result == null) return false;
                var ok = result.EditUser(newUser);
                if (!ok) return false;
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}
