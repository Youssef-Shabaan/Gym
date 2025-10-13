
using Gym.DAL.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.DAL.Entities
{
    public class Payment
    {
        public int Id { get;private set; }
        public decimal Amount { get; private set; }
        public DateTime PaymentDate { get; private set; }
        [ForeignKey("Member")]
        public string MemberId { get; private set; }
        public Member Member { get; private set; }
        public PaymentMethod paymentMethod { get; private set; }
        public bool EditPayment(Payment payment) {
            if(payment == null) return false;
            Amount = payment.Amount;
            PaymentDate = payment.PaymentDate;
            MemberId = payment.MemberId;
            paymentMethod = payment.paymentMethod;
            return true;
        }
    }
}