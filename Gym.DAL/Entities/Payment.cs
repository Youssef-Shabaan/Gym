
using Gym.DAL.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.DAL.Entities
{
    public class Payment
    {
        public Payment() { }
        public Payment(decimal amount, PaymentMethod method, DateTime paymentDate, string? transactionId, Gateway? gateway, int memberId, int? sessionId = null, int? planId = null)
        {
            Amount = amount;
            Method = method;
            PaymentDate = paymentDate;
            TransactionId = transactionId;
            Gateway = gateway;
            MemberId = memberId;
            SessionId = sessionId;
            PlanId = planId;
        }
        public int Id { get; private set; }

        public decimal Amount { get; private set; }

        public PaymentMethod Method { get; private set; }


        public DateTime PaymentDate { get; private set; }

        public string? TransactionId { get; private set; }

        public Gateway? Gateway { get; private set; }

        [ForeignKey("Member")]
        public int MemberId { get; private set; }
        public Member Member { get; private set; }

        [ForeignKey("Session")]
        public int? SessionId { get; private set; }
        public Session? Session { get; private set; }
        [ForeignKey("Plan")]
        public int? PlanId { get; private set; }
        public Plan? Plan { get; private set; }

        public bool Update(Payment payment)
        {
            this.Amount = payment.Amount;
            this.Method = payment.Method;
            this.PaymentDate = payment.PaymentDate;
            this.TransactionId = payment.TransactionId;
            this.Gateway = payment.Gateway;
            return true;
        }

    }

}