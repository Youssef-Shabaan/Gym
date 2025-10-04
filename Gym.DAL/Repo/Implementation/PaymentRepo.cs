
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
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
        public bool Create(Payment newPayment)
        {
            try
            {
                if (newPayment == null) return false;
                DB.payments.Add(newPayment);
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Delete(int id)
        {
            try
            {
                var result = DB.payments.Where(m => m.Id == id).FirstOrDefault();
                if (result == null) return false;
                DB.payments.Remove(result);
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public List<Payment> GetAll()
        {
            var result = DB.payments.ToList();
            return result;
        }

        public Payment GetById(int id)
        {
            var result = DB.payments.Where(m => m.Id == id).FirstOrDefault();
            return result;
        }

        public bool Update(Payment newPayment)
        {
            try
            {
                var result = DB.payments.Where(m => m.Id == newPayment.Id).FirstOrDefault();
                if (result == null) return false;
                var ok = result.EditPayment(newPayment);
                if (!ok) return false;
                DB.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}
