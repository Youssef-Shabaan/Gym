
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.DAL.Entities
{
    public class Plan
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal Price { get; private set; }

        // Trainer owner
        [ForeignKey("Trainer")]
        public int TrainerId { get; private set; }
        public Trainer Trainer { get; private set; }

        // Sessions included in this plan
        public List<Session> Sessions { get; set; } = new();

        // Members subscribed
        public List<MemberPlan> MemberPlans { get; set; } = new();
        public bool Update(Plan plan) {
            this.Name = plan.Name;
            this.Description = plan.Description;
            this.StartDate = plan.StartDate;
            this.EndDate = plan.EndDate;
            this.Price = plan.Price;
            return true;
        }
    }
}
