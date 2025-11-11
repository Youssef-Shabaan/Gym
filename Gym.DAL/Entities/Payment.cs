
using Gym.DAL.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.DAL.Entities
{
    public class Payment
    {
        public Payment() { }
        public Payment(decimal amount, PaymentMethod method, PaymentStatus status, DateTime paymentDate, string? transactionId, Gateway? gateway, int memberId, int? memberSessionId = null, int? trainerSubscriptionId = null)
        {
            Amount = amount;
            Method = method;
            Status = status;
            PaymentDate = paymentDate;
            TransactionId = transactionId;
            Gateway = gateway;
            MemberId = memberId;
            MemberSessionId = memberSessionId;
            TrainerSubscriptionId = trainerSubscriptionId;
        }
        public int Id { get; private set; }

        public decimal Amount { get; private set; }

        public PaymentMethod Method { get; private set; }

        public PaymentStatus Status { get; private set; }

        public DateTime PaymentDate { get; private set; }

        public string? TransactionId { get; private set; }

        public Gateway? Gateway { get; private set; }

        [ForeignKey("Member")]
        public int MemberId { get; private set; }
        public Member Member { get; private set; }

        [ForeignKey("MemberSession")]
        public int? MemberSessionId { get; private set; }
        public MemberSession? MemberSession { get; private set; }

        [ForeignKey("TrainerSubscription")]
        public int? TrainerSubscriptionId { get; private set; }
        public TrainerSubscription? TrainerSubscription { get; private set; }
        public bool Update(Payment payment)
        {
            this.Amount = payment.Amount;
            this.Method = payment.Method;
            this.Status = payment.Status;
            this.PaymentDate = payment.PaymentDate;
            this.TransactionId = payment.TransactionId;
            this.Gateway = payment.Gateway;
            return true;
        }
        public bool updateStatus(PaymentStatus Status)
        {
            this.Status = Status;
            return true;
        }
    }

}