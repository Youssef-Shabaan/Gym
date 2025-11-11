
using Gym.DAL.Entities;
using Gym.DAL.Enums;

namespace Gym.DAL.Repo.Abstraction
{
    public interface IPaymentRepo
    {
        (bool, string) Add(Payment payment);
        (bool, string, IEnumerable<Payment>) GetAll();
        (bool, string, Payment) GetById(int id);
        (bool, string, IEnumerable<Payment>) GetByMemberId(int memberId);
        (bool, string, Payment?) GetByTransactionId(string transactionId);
        (bool, string, IEnumerable<Payment>) GetSessionPayments();
        (bool, string, IEnumerable<Payment>) GetSubscriptionPayments();
        (bool, string) Update(Payment payment);
        (bool, string) UpdateStatus(int paymentId, PaymentStatus newStatus);
        (bool, string) Delete(int id);
    }
}
