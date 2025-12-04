using Gym.DAL.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;


namespace Gym.DAL.Entities
{
    public class Trainer
    {
        public Trainer() { }
        public Trainer(string name, string image, Gender gender, int age, string? address,  string UserId)
        {
            Name = name;
            if (image != null) Image = image;
            else if (gender == Gender.Male) Image = "avatar_man.png";
            else Image = "avatar_woman.png";
            JoinDate = DateTime.Now;
            IsDeleted = false;
            Age = age;
            Address = address;
            userId = UserId;
        }
        [Key]
        public int TrainerId { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string? Info { get; private set; }
        public Gender Gender { get; private set; }
        public string Image { get; private set; }
        public string? Address { get; set; }
        public DateTime? JoinDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public int Count { get; set; } = 0;


        //relation ya hussein
        [ForeignKey("User")]
        public string userId { get; private set; }
        public User User { get; private set; }
        public IEnumerable<Session> Sessions { get; private set; }

        public bool EditTrainer(Trainer trainer, string PhoneNumber)
        {
            if (trainer == null) return false;
            Name = trainer.Name;
            Image = trainer.Image;
            UpdateDate = DateTime.Now;
            Address = trainer.Address;
            Age = trainer.Age;
            User.PhoneNumber = PhoneNumber;
            return true;
        }

        public bool Delete()
        {
            if (!IsDeleted)
            {
                IsDeleted = true;
                DeletedOn = DateTime.Now;
                return true;
            }
            return false;
        }
        public bool RestoreTrainer()
        {
            if (IsDeleted == false) return false;
            else
            {
                IsDeleted = false;
                DeletedOn = null;
                return true;
            }
        }
    }
}
