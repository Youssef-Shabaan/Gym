
using Gym.DAL.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.DAL.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public PaymentMethod paymentMethod { get; set; }
    }
}