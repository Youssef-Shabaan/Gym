
using Gym.DAL.Entities;
using Gym.DAL.Enums;

namespace Gym.DAL.Repo.Abstraction
{
    public interface IPaymentRepo
    {
        bool Add(Payment payment);

        IEnumerable<Payment> GetAll();

        Payment GetById(int id);

        IEnumerable<Payment> GetByMemberId(int memberId);

        Payment? GetByTransactionId(string transactionId);

        // Payments related to sessions
        IEnumerable<Payment> GetSessionPayments();

        // Payments related to plans (NEW)
        IEnumerable<Payment> GetPlanPayments();

        bool Update(Payment payment);


        bool Delete(int id);
    }
}