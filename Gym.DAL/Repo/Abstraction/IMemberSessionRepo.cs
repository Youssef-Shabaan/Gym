using Gym.DAL.Entities;


namespace Gym.DAL.Repo.Abstraction
{
    public interface IMemberSessionRepo
    {
        (bool, string) AddMemberToSession(int memberId, int sessionId);

        (bool, string) RemoveMemberFromSession(int memberId, int sessionId);

        (bool, string,IEnumerable<Session>?) GetSessionsByMember(int memberId);

        (bool, string,IEnumerable<Member>?) GetMembersBySession(int sessionId);

        (bool, string) RemoveAllSessionsForMember(int memberId);

        (bool, string) RemoveAllMembersFromSession(int sessionId);
    }
}
