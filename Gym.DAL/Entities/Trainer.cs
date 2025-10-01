using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.Entities
{
    public class Trainer
    {
        public Trainer() { }
        public Trainer(string name, string email,string image, string? phone = null)
        {
            Name = name; 
            Email = email;
            Image = image;
            Phone = phone;
            JoinDate = DateTime.Now;
            IsDeleted = false;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Image { get; private set; }
        public string? Phone { get; private set; }
        public DateTime? JoinDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedOn { get; private set; }


        //relation ya hussein
        public IEnumerable<Session> Sessions { get; private set; }

        public bool EditTrainer(Trainer trainer)
        {
            if (trainer == null) return false;
            Name = trainer.Name;
            Email = trainer.Email;
            Image = trainer.Image;
            Phone = trainer.Phone;
            UpdateDate = DateTime.Now;
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
