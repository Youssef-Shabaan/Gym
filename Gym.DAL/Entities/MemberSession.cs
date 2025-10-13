
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.DAL.Entities
{
    public class MemberSession
    {
        public MemberSession() { }
        public MemberSession(string memberId, int sessionId) 
        {
            this.memberId = memberId;
            this.sessionId = sessionId;
        }

        [ForeignKey("_Member")]
        public string memberId { get; private set; }
        public Member _Member { get; private set; }
        [ForeignKey("_Session")]
        public int sessionId { get; private set; }
        public Session _Session { get; private set; }
    }
}
