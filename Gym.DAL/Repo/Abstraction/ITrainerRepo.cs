using Gym.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.Repo.Abstraction
{
    public interface ITrainerRepo
    {
        (bool,string) AddTrainer(Trainer trainer);
        (bool,string) DeletTrainer(int id);
        (bool,string) UpdateTrainer(Trainer trainer);
        (bool,IEnumerable<Trainer>) GetAllTrainers();
        (bool, Trainer) GetTrainerById(int id); 
    }
}
