using Gym.DAL.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.DAL.Entities
{
    public class Member : User
    {
        public Member() { }
        public Member(string name, Gender gender,string image , int age, string? address, string phone, string email)
            :base(UserType.Member, phone, email)
        {
            Name = name; 
            Gender = gender;
            Age = age;
            Image = image;
            Address = address;
            JoinDate = DateTime.Now;
            IsDeleted = false;
        }
        public string Name { get; private set; }
        public string Image { get; set; }
        public Gender Gender { get; private set; }
        public int Age { get; private set; }
        public string? Address { get; private set; }
        public DateTime? JoinDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool IsDeleted { get; private set; } 
        public DateTime? DeleltedOn { get; private set; }

        //relation ship ya hussein
        [ForeignKey("_MemberShip")]
        public int? MemberShipId { get; private set; }
        public MemberShip _MemberShip { get; private set; }
        public List<MemberSession> memberSessions { get; private set; }
        public bool EditMember(Member member)
        {
            if (member != null)
            {
                Name = member.Name;
                Gender = member.Gender;
                Age = member.Age;
                Image = member.Image;
                Address = member.Address;
                UpdateDate = member.UpdateDate;
                base.Email = member.Email;
                base.PhoneNumber = member.PhoneNumber;
                return true;
            }
            return false;
        }
        public bool Delete()
        {
            if(!IsDeleted)
            {
                IsDeleted = true;
                DeleltedOn = DateTime.Now;
                return true;
            }
            return false;
        }

        public bool RestoreMember()
        {
            if (IsDeleted)
            {
                IsDeleted = false;
                DeleltedOn = null;
                return true;
            }
            return false;
        }
    }
}
