using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

                if (result.Entity.Trainer_Id == 0)
                    return (false, "Error adding this Trainer");

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string?) UpdateTrainer(Trainer trainer)
        {
            try
            {
                var oldTrainer = GymDb.trainers.FirstOrDefault(a => a.Trainer_Id == trainer.Trainer_Id);
                if (oldTrainer == null)
                    return (false, "Trainer not found.");

                if (oldTrainer.EditTrainer(trainer))
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
                var trainer = GymDb.trainers.FirstOrDefault(x => x.Trainer_Id == id);
                if (trainer != null)
                {
                    if (trainer.Delete())
                    {
                        GymDb.SaveChanges();
                        return (true, null);
                    }
                    return (false, "This trainer is already deleted.");
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
                var trainer = GymDb.trainers.FirstOrDefault(x => x.Trainer_Id == id);
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

        public (bool, IEnumerable<Trainer>?) GetAllTrainers()
        {
            try
            {
                var trainers = GymDb.trainers.Where(t => !t.IsDeleted).ToList();

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
                var trainer = GymDb.trainers.FirstOrDefault(a => a.Trainer_Id == id);
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
                    .FirstOrDefault(t => t.Trainer_Id == trainerId);

                if (trainer == null || trainer.Sessions == null || !trainer.Sessions.Any())
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
