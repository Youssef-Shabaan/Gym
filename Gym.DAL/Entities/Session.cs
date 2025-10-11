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
        public Session(string name, string description, DateTime scheduleTime, int capactiy) 
        {
            Name = name; 
            Description = description; 
            ScheduleTime = scheduleTime;
            this.Capacity = capactiy;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public DateTime ScheduleTime { get; private set; }
        public int Count { get;  set; } = 0;
        public int Capacity { get; private set; }


        // relation ya hussein
        
        [ForeignKey("_Trainer")]
        public int? TrainerId { get; set; }
        public Trainer _Trainer { get; private set; }
        public List<MemberSession> memberSessions { get; private set; }
        public bool Update(Session session)
        {
            if (session == null) return false;
            Name = session.Name;
            Description = session.Description;
            ScheduleTime = session.ScheduleTime;
            TrainerId = session.TrainerId;
            return true;
        }
    }
}
