
using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.Session;

namespace Gym.BLL.Service.Abstraction
{
    public interface IMemberSessionService
    {
        (bool, string) AddMemberToSession(int memberId, int sessionId);
        (bool, string) RemoveMemberFromSession(int memberId, int sessionId);
        (bool, string, IEnumerable<GetSessionVM>?) GetSessionsByMember(int memberId);
        (bool, string, IEnumerable<GetMemberVM>?) GetMembersBySession(int sessionId);
        (bool, string) RemoveAllSessionsForMember(int memberId);
        (bool, string) RemoveAllMembersFromSession(int sessionId);
    }
}
