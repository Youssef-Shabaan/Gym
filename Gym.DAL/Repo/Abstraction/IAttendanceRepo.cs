
using Gym.DAL.Entities;

namespace Gym.DAL.Repo.Abstraction
{
    public interface IAttendanceRepo
    {
        List<Attendance> GetAll();
        Attendance GetById(int id);
        bool Create(Attendance newAttendance);
        bool Update(Attendance newAttendance);
        bool Delete(int id);
    }
}
