
using Gym.BLL.ModelVM.Payment;

namespace Gym.BLL.Service.Abstraction
{
    public interface IPaymentService
    {
        (bool, string) AddPayment(AddPaymentVM vm);
        (bool, string, List<GetPaymentVM>) GetAllPayments();
    }
}
