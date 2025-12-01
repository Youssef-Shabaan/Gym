
using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.MemberSession;
using Gym.BLL.ModelVM.Session;
using Gym.DAL.Entities;

namespace Gym.BLL.Service.Abstraction
{
    public interface IMemberSessionService
    {
        (bool, string) Add(AddMemberSessionVM vm);
        (bool, string) AddMemberToSession(string userId, int sessionId);
        (bool, string) Delete(int id);

        (bool, string) SetAttendance(int memberId, int sessionId);

        (bool, string) Update(UpdateMemberSessionVM memberSession);

        (bool, string, IEnumerable<GetMemberSessionVM>?) GetAll();

        (bool, string, GetMemberSessionVM?) GetById(int Id);

        (bool, string, IEnumerable<GetMemberSessionVM>?) GetByMemberId(int MemberId);
        (bool, string, IEnumerable<GetMemberSessionVM>?) GetBySessionId(int SessionId);
    }
}
