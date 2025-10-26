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
        (bool, string?) AddTrainer(Trainer trainer);
        (bool, string?) UpdateTrainer(Trainer trainer);
        (bool, string?) DeleteTrainer(int id);
        int GetTrainerCount();


        (bool, List<Trainer>?) GetAllTrainers();
        (bool, Trainer?) GetTrainerById(int id);

        (bool, Trainer?) GetTrainerSessions(int trainerId);

        (bool, string?) RestoreTrainer(int id);
    }
}
