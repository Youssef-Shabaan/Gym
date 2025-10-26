using Gym.BLL.ModelVM.Attendance;

namespace Gym.BLL.Service.Abstraction
{
    public interface IAttendanceService
    {
        (bool, string, IEnumerable<AttendanceMemberVM>) GetAttendanceMember(int sessionId);
        (bool, string) Create(CreateAttendanceVM attendanceVm);
        (bool, string) Update(UpdateAttendanceVM updateAttendanceVM);
        (bool, string, AttendanceMemberVM) GetById(int id);
        (bool, string) Delete(int id);

    }
}
