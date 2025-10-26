
using Gym.DAL.Enums;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.DAL.Entities
{
    public class Admin
    {
        public Admin() { }
        public Admin(string name, string image, Gender gender, int age, string address, string userid) {
            this.Name = name;
            this.Image = image;
            this.Gender = gender;
            this.Age = age;
            this.Address = address;
            this.UserId = userid;
            this.JoinDate = DateTime.Now;
        }
        public int AdminId { get; private set; }
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
        [ForeignKey("User")]
        public string UserId { get; private set; }
        public User User { get; private set; }
        public bool update(Admin admin) {
            this.Name = admin.Name;
            this.Image = admin.Image;
            this.Gender = admin.Gender;
            this.Age = admin.Age;
            this.Address = admin.Address;
            this.UpdateDate = DateTime.Now;
            return true;
        }

    }
}
