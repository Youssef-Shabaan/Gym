
using Gym.BLL.ModelVM.Trainer;
using Gym.DAL.Entities;

namespace Gym.BLL.Service.Abstraction
{
    public interface ITrainerService
    {
        (bool, string?) AddTrainer(AddTrainerVM trainer);
        (bool, string?) UpdateTrainer(UpdateTrainerVM trainer);
        (bool, string?) DeleteTrainer(int id);
        int GetTrainerCount();

        (bool, IEnumerable<GetTrainerVM>?) GetAllTrainers();
        (bool, GetTrainerVM?) GetTrainerById(int id);

        (bool, string, IEnumerable<GetTrainerSessionVM>?) GetTrainerSessions(int trainerId);

        (bool, string?) RestoreTrainer(int id);
    }
}
