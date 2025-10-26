
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Gym.DAL.Repo.Implementation
{
    public class AttendanceRepo : IAttendanceRepo
    {
        private readonly GymDbContext DB;
        public AttendanceRepo(GymDbContext DB)
        {
            this.DB = DB;
        }
        public bool Create(Attendance newAttendance)
        {
            try
            {
                if (newAttendance == null) return false;
                DB.attendances.Add(newAttendance);
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Delete(int id)
        {
            try
            {
                var result = DB.attendances.Where(m => m.Id == id).FirstOrDefault();
                if (result == null) return false;
                DB.attendances.Remove(result);
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public (bool, List<Attendance>) GetAttendanceMemberForSession(int sessionId)
        {
            try
            {
                var result = DB.attendances.Where(a => a.SessionId == sessionId)
                                    .Include(a => a.member).ToList(); 
                
                if (!result.Any() || result.Count() == 0)
                {
                    return (false, null);
                }
                return(true, result);
            }
            catch
            {
                return(false, null);
            }
        }

        public Attendance GetById(int id)
        {
            var result = DB.attendances.Include(m => m.member).Where(m => m.Id == id).FirstOrDefault();
            return result;
        }

        public bool Update(Attendance newAttendance)
        {
            try
            {
                var result = DB.attendances.FirstOrDefault(m => m.Id == newAttendance.Id);
                if (result == null) return false;
                var ok = result.EditAttendance(newAttendance);
                if (!ok) return false;
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}
