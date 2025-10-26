
using Gym.DAL.Entities;

namespace Gym.DAL.Repo.Abstraction
{
    public interface IAttendanceRepo
    {
        (bool, List<Attendance>) GetAttendanceMemberForSession(int sessionId);
        Attendance GetById(int id);
        bool Create(Attendance newAttendance);
        bool Update(Attendance newAttendance);
        bool Delete(int id);
    }
}
