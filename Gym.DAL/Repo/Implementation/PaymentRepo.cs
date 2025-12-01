
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Enums;
using Gym.DAL.Repo.Abstraction;

namespace Gym.DAL.Repo.Implementation
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly GymDbContext DB;
        public PaymentRepo(GymDbContext DB)
        {
            this.DB = DB;
        }

        public bool Add(Payment payment)
        {
            try
            {
                var existingPayment = DB.payments
                    .FirstOrDefault(p => p.TransactionId == payment.TransactionId);
                if (existingPayment != null)
                {
                    throw new Exception("Payment with the same TransactionId already exists.");
                }
                DB.payments.Add(payment);
                DB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var payment = DB.payments.FirstOrDefault(p => p.Id == id);
                if (payment == null)
                {
                    throw new Exception("Payment not found.");
                }
                DB.payments.Remove(payment);
                DB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Payment> GetAll()
        {
            try
            {
                var payments = DB.payments.ToList();
                return payments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Payment GetById(int id)
        {
            try
            {
                var payment = DB.payments.FirstOrDefault(p => p.Id == id);
                if (payment == null)
                {
                    throw new Exception("Payment not found.");
                }
                return payment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Payment> GetByMemberId(int memberId)
        {
            try
            {
                var payments = DB.payments.Where(p => p.MemberId == memberId).ToList();
                return payments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Payment? GetByTransactionId(string transactionId)
        {
            try
            {
                var payment = DB.payments.FirstOrDefault(p => p.TransactionId == transactionId);
                return payment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Payment> GetPlanPayments()
        {
            try
            {
                var payments = DB.payments
                    .Where(p => p.PlanId != null)
                    .ToList();
                return payments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Payment> GetSessionPayments()
        {
            try
            {
                var payments = DB.payments
                    .Where(p => p.SessionId != null)
                    .ToList();
                return payments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Update(Payment payment)
        {
            try
            {
                var existingPayment = DB.payments.FirstOrDefault(p => p.Id == payment.Id);
                if (existingPayment == null)
                {
                    throw new Exception("Payment not found.");
                }
                existingPayment.Update(payment);
                DB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

     
    }
}
