
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Gym.DAL.Repo.Implementation
{
    public class TrainerSubscriptionRepo : ITrainerSubscriptionRepo
    {
        private readonly GymDbContext DB;
        public TrainerSubscriptionRepo(GymDbContext DB)
        {
            this.DB = DB;
        }
        public (bool, string) Add(TrainerSubscription subscription)
        {
            try {
                DB.trainerSubscriptions.Add(subscription);
                DB.SaveChanges();
                return (true, "Subscription added successfully.");
            }
            catch(Exception ex) { return (false, ex.Message); }
        }

        public (bool, string) Delete(int id)
        {
            try {
                var subscription = DB.trainerSubscriptions.FirstOrDefault(m=>m.Id==id);
                if (subscription == null)
                    return (false, "Subscription not found.");
                DB.trainerSubscriptions.Remove(subscription);
                DB.SaveChanges();
                return (true, "Subscription deleted successfully.");
            }
            catch (Exception ex) { return (false, ex.Message); }
        }

        public (bool, string, IEnumerable<TrainerSubscription>) GetAll()
        {
            try {
                var subscriptions = DB.trainerSubscriptions
                    .Include(p=>p.Payment)
                    .Include(t=>t.Trainer)
                    .Include(m=>m.Member)
                    .ToList();
                return (true, "Subscriptions retrieved successfully.", subscriptions);
            }
            catch (Exception ex) { return (false, ex.Message, null); }
        }

        public (bool, string, TrainerSubscription) GetById(int id)
        {
            try {
                var subscription = DB.trainerSubscriptions
                    .Include(p => p.Payment)
                    .Include(t => t.Trainer)
                    .Include(m => m.Member)
                    .FirstOrDefault(s => s.Id == id);
                if (subscription == null)
                    return (false, "Subscription not found.", null);
                return (true, "Subscription retrieved successfully.", subscription);
            }
            catch (Exception ex) { return (false, ex.Message, null); }
        }

        public (bool, string, IEnumerable<TrainerSubscription>) GetByMemberId(int memberId)
        {
            try {
                var subscriptions = DB.trainerSubscriptions
                    .Include(p => p.Payment)
                    .Include(t => t.Trainer)
                    .Include(m => m.Member)
                    .Where(s => s.MemberId == memberId)
                    .ToList();
                return (true, "Subscriptions retrieved successfully.", subscriptions);
            }
            catch (Exception ex) { return (false, ex.Message, null); }
        }

        public (bool, string, IEnumerable<TrainerSubscription>) GetByTrainerId(int trainerId)
        {
            try {
                var subscriptions = DB.trainerSubscriptions
                    .Include(p => p.Payment)
                    .Include(t => t.Trainer)
                    .Include(m => m.Member)
                    .Where(s => s.TrainerId == trainerId)
                    .ToList();
                return (true, "Subscriptions retrieved successfully.", subscriptions);
            }
            catch (Exception ex) { return (false, ex.Message, null); }
        }

        public (bool, string) Update(TrainerSubscription subscription)
        {
            try {
                var existingSubscription = DB.trainerSubscriptions.FirstOrDefault(s => s.Id == subscription.Id);
                if (existingSubscription == null)
                    return (false, "Subscription not found.");
                existingSubscription.Update(subscription);
                DB.SaveChanges();
                return (true, "Subscription updated successfully.");
            }
            catch (Exception ex) { return (false, ex.Message);}
        }
    }
}
