using Gym.DAL.Enums;

namespace Gym.BLL.ModelVM.Member
{
    public class GetMemberVM
    {
        public int MemberId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public string Address { get; set; }

        public string? Image { get; set; }

        public DateTime? JoinDate { get; set; }

        public bool IsDeleted { get; set; }

        public string? Email { get; set; }

        public string? UserName { get; set; }
    }
}
