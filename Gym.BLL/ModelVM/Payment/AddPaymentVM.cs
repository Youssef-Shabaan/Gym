
using Gym.DAL.Enums;

namespace Gym.BLL.ModelVM.Payment
{
    public class AddPaymentVM
    {
        public decimal Amount { get; set; }

        public PaymentMethod Method { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public string? TransactionId { get; set; }

        public Gateway? Gateway { get; set; }

        public int MemberId { get; set; }

        public int? SessionId { get; set; }

        public int? PlanId { get; set; }
    }
}
