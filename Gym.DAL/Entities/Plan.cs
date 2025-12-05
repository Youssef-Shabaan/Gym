
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.DAL.Entities
{
    public class Plan
    {
        public Plan()
        {
            
        }
        public Plan(string name, string description, DateTime startDate, DateTime endDate, decimal price, int capacity, int booked)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Price = price;
            Capcity = capacity;
            Booked = booked;
        }
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal Price { get; private set; }
        public int Capcity { get; private set; }
        public int Booked { get; private set; }

        // Trainer owner
        [ForeignKey("Trainer")]
        public int TrainerId { get; private set; }
        public Trainer Trainer { get; private set; }

        // Sessions included in this plan
        public List<Session> Sessions { get; set; } = new();

        // Members subscribed
        public List<MemberPlan> MemberPlans { get; set; } = new();
        public bool Update(Plan plan)
        {
            this.Name = plan.Name;
            this.Description = plan.Description;
            this.StartDate = plan.StartDate;
            this.EndDate = plan.EndDate;
            this.Price = plan.Price;
            this.Capcity = plan.Capcity;
            return true;
        }
        public bool Book()
        {
            if (Booked >= Capcity) return false;
            Booked += 1;
            return true;
        }
        public void Cancel()
        {
            if(Booked > 0)
                Booked--;
        }
    }
}
