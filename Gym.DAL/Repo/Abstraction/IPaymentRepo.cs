
using Gym.DAL.Entities;

namespace Gym.DAL.Repo.Abstraction
{
    public interface IPaymentRepo
    {
        List<Payment> GetAll();
        Payment GetById(int id);
        bool Create(Payment newPayment);
        bool Update(Payment newPayment);
        bool Delete(int id);
    }
}
