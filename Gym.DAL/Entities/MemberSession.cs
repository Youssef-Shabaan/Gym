
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.DAL.Entities
{
    public class MemberSession
    {

        [ForeignKey("_Member")]
        public int memberId { get; private set; }
        public Member _Member { get; private set; }
        [ForeignKey("_Session")]
        public int sessionId { get; private set; }
        public Session _Session { get; private set; }
    }
}
