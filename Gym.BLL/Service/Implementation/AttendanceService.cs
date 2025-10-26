using AutoMapper;
using Gym.BLL.ModelVM.Attendance;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;

namespace Gym.BLL.Service.Implementation
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepo attendanceRepo;
        private readonly IMapper mapper;

        public AttendanceService(IAttendanceRepo attendanceRepo, IMapper mapper)
        {
            this.attendanceRepo = attendanceRepo;
            this.mapper = mapper;
        }

        public (bool, string) Create(CreateAttendanceVM attendanceVm)
        {
            try
            {
                var attend = mapper.Map<Attendance>(attendanceVm);
                var result = attendanceRepo.Create(attend);
                if (!result) return (false, "Error in attend this session");
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, "Error in attend this session");
            }
        }

        public (bool, string) Delete(int id)
        {
            try
            {
                var result = attendanceRepo.Delete(id);
                if (!result) return (false, "Error in deleting operation");
                return (true, null);
            }
            catch(Exception ex)
            {
                return (false, "Error in deleting operation");
            }
        }

        public (bool, string, IEnumerable<AttendanceMemberVM>) GetAttendanceMember(int sessionId)
        {
            try
            {
                var attendanceMember = attendanceRepo.GetAttendanceMemberForSession(sessionId);
                if (!attendanceMember.Item1) return (false, "No member attend to this session", null);
                var result  = mapper.Map<IEnumerable<AttendanceMemberVM>>(attendanceMember.Item2);
                return(true, null, result);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, AttendanceMemberVM) GetById(int id)
        {
            try
            {
                var attend = attendanceRepo.GetById(id);
                var result = mapper.Map<AttendanceMemberVM>(attend);
                return(true, null, result);
            }
            catch(Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string) Update(UpdateAttendanceVM updateAttendanceVM)
        {
            try
            {
                var attend = mapper.Map<Attendance>(updateAttendanceVM);
                var result = attendanceRepo.Update(attend);
                if (!result) return (false, "Error in updating operation");
                return (true, null);
            }
            catch(Exception ex)
            {
                return (false, "Error in updating opration");
            }
        }
    }
}
