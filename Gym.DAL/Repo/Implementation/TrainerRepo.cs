using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.Repo.Implementation
{
    public class TrainerRepo : ITrainerRepo
    {
        private readonly GymDbContext GymDb;
        public TrainerRepo(GymDbContext GymDb)
        {
            this.GymDb = GymDb;
        }
        public (bool, string?) AddTrainer(Trainer trainer)
        {
            try
            {
                var result = GymDb.trainers.Add(trainer);
                GymDb.SaveChanges();
                if(result.Entity.Id == 0)
                {
                    return (false, "Error adding this Trainer.");
                }
                return (true, null);
            }
            catch (Exception ex)
            {
                return(false, "Error adding this Trainer.");
            }
        }

        public (bool, string?) DeletTrainer(int id)
        {
            try
            {
                var trainer = GymDb.trainers.FirstOrDefault(x => x.Id == id);
                if(trainer != null)
                {
                    if(trainer.Delete())
                    {
                        GymDb.SaveChanges();
                        return (true, null);
                    }
                    return (false, "This trainer is already deleted.");
                }
                return(false, "May be this Trainer not found.");
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, IEnumerable<Trainer>?) GetAllTrainers()
        {
            try
            {
                var trainers = GymDb.trainers
                    .Where(t => !t.IsDeleted)
                    .ToList();

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
                var trainer = GymDb.trainers.FirstOrDefault(a => a.Id == id); 
                if(trainer != null)
                {
                    return(true, trainer);
                }
                return(false, null);
            }
            catch( Exception ex )
            {
                return (false, null);
            }
        }

        public (bool, string?) RestoreTrainer(int id)
        {
            try
            {
                var trainer = GymDb.trainers.Where(a => a.Id == id).FirstOrDefault();
                if (trainer == null) return (false, "This trainer not exists.");
                if(trainer.RestoreTrainer())
                {
                    return (true, null);
                }
                return (false, "This trainer already exists.");
            }
            catch(Exception ex)
            {
                return (false, null);
            }
        }

        public (bool, string?) UpdateTrainer(Trainer trainer)
        {
            try
            {
                var oldTrainer = GymDb.trainers.Where(a => a.Id == trainer.Id).FirstOrDefault();
                if(oldTrainer == null)
                {
                    return(false, "May be this Trainer not found.");
                }
                if(oldTrainer.EditTrainer(trainer))
                {
                    GymDb.SaveChanges();
                    return (true, null);
                }
                return (false, "Some thig goes wrong.");
            }
            catch (Exception ex)
            {
                return(false, ex.Message);
            }
        }
    }
}
