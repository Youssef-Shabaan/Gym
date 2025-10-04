
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;

namespace Gym.DAL.Repo.Implementation
{
    public class MemberRepo : IMemberRepo
    {
        private readonly GymDbContext DB;
        public MemberRepo(GymDbContext DB)
        {
            this.DB = DB;
        }
        public bool Create(Member newMember)
        {
            try
            {
                if (newMember == null) return false;
                DB.members.Add(newMember);
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Delete(int id)
        {
            try
            {
                var result = DB.members.Where(m => m.Id == id).FirstOrDefault();
                if (result == null) return false;
                DB.members.Remove(result);
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public List<Member> GetAll()
        {
            var result = DB.members.ToList();
            return result;
        }

        public Member GetById(int id)
        {
            var result = DB.members.Where(m => m.Id == id).FirstOrDefault();
            return result;
        }

        public bool Update(Member newMember)
        {
            var result = DB.members.Where(m => m.Id == newMember.Id).FirstOrDefault();
            if (result == null) return false;
            var ok = result.EditMember(newMember);
            if (!ok) return false;
            DB.SaveChanges();
            return true;
        }
    }
}
