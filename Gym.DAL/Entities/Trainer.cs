using Gym.DAL.Enums;
using System.ComponentModel.DataAnnotations;


namespace Gym.DAL.Entities
{
    public class Trainer : User
    {
        public Trainer() { }
        public Trainer(string name, string image, int age, string? info, string? address, int capacity): base(UserType.Trainer)
        {
            Name = name;
            Image = image;
            JoinDate = DateTime.Now;
            IsDeleted = false;
            Age = age;
            Info = info;
            Address = address;
            Capacity = capacity;
        }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string? Info { get; private set; }
        public string Image { get; private set; }
        public string? Address { get; set; }
        public DateTime? JoinDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public int Count { get; set; } = 0;
        public int Capacity { get; private set; }


        //relation ya hussein
        public IEnumerable<Session> Sessions { get; private set; }

        public bool EditTrainer(Trainer trainer)
        {
            if (trainer == null) return false;
            Name = trainer.Name;
            Image = trainer.Image;
            UpdateDate = DateTime.Now;
            Address = trainer.Address;
            Info = trainer.Info;
            Age = trainer.Age;
            Capacity = trainer.Capacity;
            base.Email = trainer.Email;
            base.PhoneNumber = trainer.PhoneNumber;
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
