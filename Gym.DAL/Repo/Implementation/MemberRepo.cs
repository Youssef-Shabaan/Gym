
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Gym.DAL.Repo.Implementation
{
    public class MemberRepo : IMemberRepo
    {
        private readonly GymDbContext DB;
        public MemberRepo(GymDbContext DB)
        {
            this.DB = DB;
        }

        public bool ChangePhoto(Member MemberImagePath)
        {
            try
            {
                var member = DB.members.FirstOrDefault(m => m.MemberId == MemberImagePath.MemberId);
                if (member == null) return false;
                var result = member.ChangePhoto(MemberImagePath);
                DB.SaveChanges();
                return result;
            }
            catch (Exception ex) 
            {
                return false;
            }
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
                var result = DB.members.Where(m => m.MemberId == id).FirstOrDefault();
                if (result == null) return false;
                DB.members.Remove(result);
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public void DeletePhoto(int id)
        {
            var member = DB.members.FirstOrDefault(m => m.MemberId == id);
            member.DeletePhoto();
            DB.SaveChanges();
        }

        public List<Member> GetAll()
        {
            var result = DB.members.Include(m => m.User).ToList();
            return result;
        }

        public Member GetById(int id)
        {
            var result = DB.members.Include(m => m.User).Where(m => m.MemberId == id).FirstOrDefault();
            return result;
        }
        public Member GetByUserId(string id)
        {
            var result = DB.members.Where(m => m.UserId == id).FirstOrDefault();
            return result;
        }

        public bool Update(Member newMember, string phoneNumber)
        {
            var result = DB.members.Where(m => m.UserId == newMember.UserId).FirstOrDefault();
            if (result == null) return false;
            var ok = result.EditMember(newMember, phoneNumber);
            if (!ok) return false;
            DB.SaveChanges();
            return true;
        }
    }
}
