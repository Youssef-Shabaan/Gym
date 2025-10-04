
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;

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
                var result = DB.attendances.Where(m => m.id == id).FirstOrDefault();
                if (result == null) return false;
                DB.attendances.Remove(result);
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public List<Attendance> GetAll()
        {
            var result = DB.attendances.ToList();
            return result;
        }

        public Attendance GetById(int id)
        {
            var result = DB.attendances.Where(m => m.id == id).FirstOrDefault();
            return result;
        }

        public bool Update(Attendance newAttendance)
        {
            try
            {
                var result = DB.attendances.Where(m => m.id == newAttendance.id).FirstOrDefault();
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
