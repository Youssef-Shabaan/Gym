
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Gym.DAL.Repo.Implementation
{
    public class MemberShipRepo : IMemberShipRepo
    {
        private readonly GymDbContext DB;
        public MemberShipRepo(GymDbContext DB)
        {
            this.DB = DB;
        }
        public bool Create(MemberShip newMemberShip)
        {
            try
            {
                if (newMemberShip == null) return false;
                DB.memberShips.Add(newMemberShip);
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Delete(int id)
        {
            try {
                var result = DB.memberShips.Where(m => m.Id == id).FirstOrDefault();
                if (result == null) return false;
                DB.memberShips.Remove(result);
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public (bool, string ,List<MemberShip>) GetAll()
        {
            var result = DB.memberShips.Include(m => m.Members).ToList();
            if(!result.Any())
            {
                return (false, "There are no member ship.", null); 
            }
            return (true, null, result);
        }

        public (bool,MemberShip) GetById(int id)
        {
            var result = DB.memberShips.Include(m => m.Members).Where(m => m.Id == id).FirstOrDefault();
            if (result == null) return (false, null);
            return (true, result);
        }

        public bool Update(MemberShip newMemberShip)
        {
            try {
                var result = DB.memberShips.Where(m => m.Id == newMemberShip.Id).FirstOrDefault();
                if (result == null) return false;
                var ok = result.UpdateMemberShip(newMemberShip);
                if (!ok) return false;
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}
