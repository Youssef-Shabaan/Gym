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
        public Session(string name, string description, DateTime StartTime,DateTime EndTime, int capactiy) 
        {
            Name = name; 
            Description = description; 
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }


        // relation ya hussein
        
        [ForeignKey("_Trainer")]
        public string TrainerId { get; set; }
        public Trainer _Trainer { get; private set; }
        public List<MemberSession> memberSessions { get; private set; }
        public bool Update(Session session)
        {
            if (session == null) return false;
            Name = session.Name;
            Description = session.Description;
            StartTime = session.StartTime;
            EndTime = session.EndTime;
            TrainerId = session.TrainerId;
            return true;
        }
    }
}