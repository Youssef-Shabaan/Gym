using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.Entities
{
    public class Trainer
    {
        public Trainer() { }
        public Trainer(string name, string image, int age, string address)
        {
            Name = name;
            Image = image;
            JoinDate = DateTime.Now;
            IsDeleted = false;
            Age = age;
            Address = address;
        }
        [Key]
        public int Trainer_Id { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Image { get; private set; }
        public string Address { get; set; }
        public DateTime? JoinDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedOn { get; private set; }


        //relation ya hussein
        public IEnumerable<Session> Sessions { get; private set; }
        [ForeignKey("User")]
        public string UserId { get; private set; }
        public User User { get; private set; }
        public bool EditTrainer(Trainer trainer)
        {
            if (trainer == null) return false;
            Name = trainer.Name;

            Image = trainer.Image;
            UpdateDate = DateTime.Now;
            Address = trainer.Address;
            Age = trainer.Age;
            return true;
        }

        public bool Delete()
        {
            if(!IsDeleted)
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
