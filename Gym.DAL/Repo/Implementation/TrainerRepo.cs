using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Gym.DAL.Repo.Implementation
{
    public class TrainerRepo : ITrainerRepo
    {
        private readonly GymDbContext GymDb;

        public TrainerRepo(GymDbContext gymDb)
        {
            GymDb = gymDb;
        }

        public (bool, string?) AddTrainer(Trainer trainer)
        {
            try
            {
                var result = GymDb.trainers.Add(trainer);
                GymDb.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string?) UpdateTrainer(Trainer trainer, string PhoneNumber)
        {
            try
            {
                var oldTrainer = GymDb.trainers.FirstOrDefault(a => a.userId == trainer.userId);
                if (oldTrainer == null)
                    return (false, "Trainer not found.");

                if (oldTrainer.EditTrainer(trainer, PhoneNumber))
                {
                    GymDb.SaveChanges();
                    return (true, null);
                }

                return (false, "Something went wrong.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string?) DeleteTrainer(int id)
        {
            try
            {
                var trainer = GymDb.trainers.FirstOrDefault(x => x.TrainerId == id);
                if (trainer != null)
                {
                    GymDb.SaveChanges();
                    return (true, null);
                }
                return (false, "Trainer not found.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string?) RestoreTrainer(int id)
        {
            try
            {
                var trainer = GymDb.trainers.FirstOrDefault(x => x.TrainerId == id);
                if (trainer == null)
                    return (false, "Trainer not found.");

                if (trainer.RestoreTrainer())
                {
                    GymDb.SaveChanges();
                    return (true, null);
                }

                return (false, "Trainer is already active.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, List<Trainer>?) GetAllTrainers()
        {
            try
            {
                var trainers = GymDb.trainers.Include(u => u.User)
                    .Where(t => !t.IsDeleted).ToList();

                if (!trainers.Any())
                    return (false, null);

                return (true, trainers);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }


        public (bool, Trainer?) GetTrainerById(int id)
        {
            try
            {
                var trainer = GymDb.trainers.Include(a => a.Sessions).Include(u => u.User)
                    .FirstOrDefault(a => a.TrainerId == id);
                if (trainer != null)
                    return (true, trainer);

                return (false, null);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }
        public (bool, Trainer?) GetTrainerByUserId(string id)
        {
            try
            {
                var trainer = GymDb.trainers.Include(a => a.Sessions).FirstOrDefault(a => a.userId == id);
                if (trainer != null)
                    return (true, trainer);

                return (false, null);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public (bool, Trainer?) GetTrainerSessions(int trainerId)
        {
            try
            {
                var trainer = GymDb.trainers
                    .Include(t => t.Sessions)
                    .FirstOrDefault(t => t.TrainerId == trainerId);

                if (trainer == null)
                    return (false, null);

                return (true, trainer);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public int GetTrainerCount()
        {
            return GymDb.trainers.Count(a => !a.IsDeleted);
        }
    }
}
