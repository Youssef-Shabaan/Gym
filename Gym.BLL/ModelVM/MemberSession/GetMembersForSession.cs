
namespace Gym.BLL.ModelVM.MemberSession
{
    public class GetMembersForSession
    {
        public int MemberSessionId { get; set; }
        public int SessionId { get; set; }
        public string MemberPhoto { get; set; }
        public string MemberUserName { get; set; }
        public string MemberEmail { get; set; } 
        public string MemberPhone { get; set; }
        public string MemberAddress { get; set; }   
        public string MemberAge { get; set; }
        public string MemberGender { get; set; }
    }
}
