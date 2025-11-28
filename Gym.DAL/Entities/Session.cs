using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.Entities
{
    public class Session
    {
        public Session() { }
        public Session(string name, string description, DateTime StartTime,DateTime EndTime, int capactiy, int booked, decimal price) 
        {
            Name = name; 
            Description = description; 
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.Price = price;
            this.Capactiy = capactiy;
            this.Booked = booked;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public decimal Price { get; private set; }  
        public int Capactiy { get; private set; }
        public int Booked { get; private set; } 

        // relation ya hussein
        
        [ForeignKey("_Trainer")]
        public int TrainerId { get; set; }
        public Trainer _Trainer { get; private set; }
        public List<MemberSession> memberSessions { get; private set; }
        public List<Attendance> Attendances { get; private set; }
        [ForeignKey("Plan")]
        public int? PlanId { get; set; }
        public Plan Plan { get; set; }

        public bool Update(Session session)
        {
            if (session == null) return false;
            Name = session.Name;
            Description = session.Description;
            StartTime = session.StartTime;
            EndTime = session.EndTime;
            this.Price = session.Price;
            this.Capactiy = session.Capactiy;
            return true;
        }

        public bool Book()
        {
            if(Booked >= Capactiy) return false;
            Booked += 1;
            return true;
        }

        public void Cancel()
        {
            Booked -= 1;
        }
    }
}