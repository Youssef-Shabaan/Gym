
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.DAL.Entities
{
    public class TrainerSubscription
    {
        public TrainerSubscription() { }

        public TrainerSubscription(decimal price, bool isActive, DateTime startTime, DateTime endTime) 
        {
            Price = price;
            IsActive = isActive;
            StartTime = startTime;
            EndTime = endTime;
        }
        public int Id { get; private set; }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public decimal Price { get; private set; }

        public bool IsActive { get; private set; }


        [ForeignKey("Payment")]
        public int? PaymentId { get; private set; }
        public Payment? Payment { get; private set; }

        [ForeignKey("Member")]
        public int MemberId { get;  private set; }
        public Member Member { get; private set; }

        [ForeignKey("Trainer")]
        public int TrainerId { get; private set; }
        public Trainer Trainer { get; private set; }

        public bool Update(TrainerSubscription trainerSubscription)
        {
            this.Price = trainerSubscription.Price;
            this.IsActive = trainerSubscription.IsActive;
            this.StartTime = trainerSubscription.StartTime;
            this.EndTime = trainerSubscription.EndTime;
            return true;
        }
    }

}
