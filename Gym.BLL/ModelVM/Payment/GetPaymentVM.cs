using Gym.DAL.Enums;

namespace Gym.BLL.ModelVM.Payment
{
    public class GetPaymentVM
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionId { get; set; }
        public int MemberId { get; set; }
        public Gateway Gateway { get;  set; }
        public string MemberName { get; set; }
    }
}
