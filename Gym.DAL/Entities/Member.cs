using Gym.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.Entities
{
    public class Member
    {
        public Member() { }
        public Member(string name, string email, Gender gender,string image , int? age = null, string? phone = null, string? address = null)
        {
            Name = name; 
            Email = email;
            Gender = gender;
            Age = age;
            Image = image;
            Phone = phone;
            Address = address;
            JoinDate = DateTime.Now;
            IsDeleted = false;
        }
        
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Image { get; set; }
        public Gender Gender { get; private set; }
        public int? Age { get; private set; }
        public string? Phone { get; private set; }   
        public string? Address { get; private set; }
        public DateTime? JoinDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool IsDeleted { get; private set; } 
        public DateTime? DeleltedOn { get; private set; }

        //relation ship ya hussein
        [ForeignKey("_MemberShip")]
        public int? MemberShipId { get; private set; }
        public MemberShip _MemberShip { get; private set; }


        public bool EditMember(Member member)
        {
            if (member != null)
            {
                Name = member.Name;
                Email = member.Email;
                Gender = member.Gender;
                Age = member.Age;
                Image = member.Image;
                Phone = member.Phone;
                Address = member.Address;
                UpdateDate = member.UpdateDate;
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
