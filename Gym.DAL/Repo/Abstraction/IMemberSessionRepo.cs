using Gym.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.Repo.Abstraction
{
    public interface IMemberSessionRepo
    {
        // Add new MemberSession (عضو ينضم إلى سيشن)
        bool AddMemberToSession(int memberId, int sessionId);

        // Remove Member from Session (عضو ينسحب من سيشن)
        bool RemoveMemberFromSession(int memberId, int sessionId);

        
        // Get all sessions for a specific member
        (bool,IEnumerable<Session>?) GetSessionsByMember(int memberId);

        // Get all members in a specific session
        (bool,IEnumerable<Member>?) GetMembersBySession(int sessionId);

        // Remove all sessions for a specific member (مثلاً لو ألغى اشتراكه)
        bool RemoveAllSessionsForMember(int memberId);

        // Remove all members from a specific session (لو السيشن اتقفل)
        bool RemoveAllMembersFromSession(int sessionId);
    }
}
