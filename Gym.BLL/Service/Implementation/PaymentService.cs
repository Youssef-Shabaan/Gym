
using AutoMapper;
using Gym.BLL.ModelVM.Payment;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;

namespace Gym.BLL.Service.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepo paymentRepo;
        private readonly IMapper mapper;
        public PaymentService(IPaymentRepo paymentRepo, IMapper mapper)
        {
            this.paymentRepo = paymentRepo;
            this.mapper = mapper;
        }
        public (bool, string) AddPayment(AddPaymentVM vm)
        {
            try
            {
                var payment = mapper.Map<Payment>(vm);
                var result = paymentRepo.Add(payment);
                if (!result)
                    return (false, "Field to add");
                return (true, "Payment added successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
