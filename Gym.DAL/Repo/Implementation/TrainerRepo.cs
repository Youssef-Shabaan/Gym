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
                if(result.Entity.Id == 0)
                {
                    return (false, "Error adding this Trainer");
                }
                GymDb.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return(false, "Error adding this Trainer");
            }
        }

        public (bool, string) DeletTrainer(int id)
        {
            throw new NotImplementedException();
        }

        public (bool, IEnumerable<Trainer>) GetAllTrainers()
        {
            throw new NotImplementedException();
        }

        public (bool, Trainer) GetTrainerById(int id)
        {
            throw new NotImplementedException();
        }

        public (bool, string) UpdateTrainer(Trainer trainer)
        {
            throw new NotImplementedException();
        }
    }
}
