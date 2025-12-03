using Gym.DAL.Entities;


namespace Gym.DAL.Repo.Abstraction
{
    public interface IMemberSessionRepo
    {
        (bool, string) Add(MemberSession memberSession);
        Member GetMemberByUserId(string userid);
        Session GetSessionById(int sessionId);
        bool IsMemberBooked(int memberId, int sessionId);
        (bool, string) Delete(int id);

        (bool , string) Update(MemberSession memberSession);
        (bool, string) SetAttendance(int memberId, int sessionId);

        (bool, string,IEnumerable<MemberSession>?) GetAll();

        (bool, string, MemberSession?) GetById(int Id);

        (bool, string, IEnumerable<MemberSession>?) GetByMemberId(int MemberId);    
        (bool, string, IEnumerable<MemberSession>?) GetBySessionId(int SessionId);

        (bool, string, IEnumerable<MemberSession>) GetMembersForSession(int sessionId);
        int GetSessionId(int memberSessionId);

    }
}
