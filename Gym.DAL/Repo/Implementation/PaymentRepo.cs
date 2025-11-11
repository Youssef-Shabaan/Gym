
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Enums;
using Gym.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Gym.DAL.Repo.Implementation
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly GymDbContext DB;
        public PaymentRepo(GymDbContext DB)
        {
            this.DB = DB;
        }

        public (bool, string) Add(Payment payment)
        {
            try
            {
                DB.payments.Add(payment);
                DB.SaveChanges();
                return (true, "Payment added successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string) Delete(int id)
        {
            try
            {
                var payment = DB.payments.FirstOrDefault(m => m.Id == id);
                if (payment == null)
                {
                    return (false, "Payment not found.");
                }
                DB.payments.Remove(payment);
                DB.SaveChanges();
                return (true, "Payment deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string, IEnumerable<Payment>) GetAll()
        {
            try
            {
                var result = DB.payments
                    .Include(m => m.Member)
                    .Include(ms => ms.MemberSession)
                    .Include(ts => ts.TrainerSubscription)
                    .ToList();
                return (true, "Payments retrieved successfully.", result);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, Payment) GetById(int id)
        {
            try
            {
                var payment = DB.payments
                    .Include(m => m.Member)
                    .Include(ms => ms.MemberSession)
                    .Include(ts => ts.TrainerSubscription)
                    .FirstOrDefault(m => m.Id == id);
                if (payment == null)
                {
                    return (false, "Payment not found.", null);
                }
                return (true, "Payment retrieved successfully.", payment);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, IEnumerable<Payment>) GetByMemberId(int memberId)
        {
            try
            {
                var payments = DB.payments
                    .Include(m => m.Member)
                    .Include(ms => ms.MemberSession)
                    .Include(ts => ts.TrainerSubscription)
                    .Where(m => m.MemberId == memberId).ToList();
                return (true, "Payments retrieved successfully.", payments);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, Payment?) GetByTransactionId(string transactionId)
        {
            try
            {
                var payment = DB.payments
                    .Include(m => m.Member)
                    .Include(ms => ms.MemberSession)
                    .Include(ts => ts.TrainerSubscription)
                    .FirstOrDefault(m => m.TransactionId == transactionId);
                if (payment == null)
                {
                    return (false, "Payment not found.", null);
                }
                return (true, "Payment retrieved successfully.", payment);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, IEnumerable<Payment>) GetSessionPayments()
        {
            try
            {
                var payments = DB.payments
                    .Include(m => m.Member)
                    .Include(ms => ms.MemberSession)
                    .Include(ts => ts.TrainerSubscription)
                    .Where(m => m.MemberSessionId != null).ToList();
                return (true, "Session payments retrieved successfully.", payments);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, IEnumerable<Payment>) GetSubscriptionPayments()
        {
            try
            {
                var payments = DB.payments
                    .Include(m => m.Member)
                    .Include(ms => ms.MemberSession)
                    .Include(ts => ts.TrainerSubscription)
                    .Where(m => m.TrainerSubscriptionId != null).ToList();
                return (true, "Subscription payments retrieved successfully.", payments);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string) Update(Payment payment)
        {
            try
            {
                var existingPayment = DB.payments.FirstOrDefault(m => m.Id == payment.Id);
                if (existingPayment == null)
                {
                    return (false, "Payment not found.");
                }
                existingPayment.Update(payment);
                DB.SaveChanges();
                return (true, "Payment updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string) UpdateStatus(int paymentId, PaymentStatus newStatus)
        {
            try
            {
                var existingPayment = DB.payments.FirstOrDefault(m => m.Id == paymentId);
                if (existingPayment == null)
                {
                    return (false, "Payment not found.");
                }
                existingPayment.updateStatus(newStatus);
                DB.SaveChanges();
                return (true, "Payment status updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
