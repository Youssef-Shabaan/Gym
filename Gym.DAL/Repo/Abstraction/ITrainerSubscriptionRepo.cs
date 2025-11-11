
using Gym.DAL.Entities;

namespace Gym.DAL.Repo.Abstraction
{
    public interface ITrainerSubscriptionRepo
    {
        (bool, string) Add(TrainerSubscription subscription);
        (bool, string, IEnumerable<TrainerSubscription>) GetAll();
        (bool, string, TrainerSubscription) GetById(int id);
        (bool, string, IEnumerable<TrainerSubscription>) GetByMemberId(int memberId);
        (bool, string, IEnumerable<TrainerSubscription>) GetByTrainerId(int trainerId);
        (bool, string) Update(TrainerSubscription subscription);
        (bool, string) Delete(int id);
    }
}