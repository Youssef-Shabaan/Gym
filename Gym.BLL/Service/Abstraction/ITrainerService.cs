
using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.Session;
using Gym.BLL.ModelVM.Trainer;
using Gym.DAL.Entities;

namespace Gym.BLL.Service.Abstraction
{
    public interface ITrainerService
    {
        (bool, string, List<GetTrainerVM>) GetAll();
        (bool, string, GetTrainerVM) GetByID(int id);
        (bool, string, GetTrainerVM) GetByUserID(string id);

		Task<(bool, string)> Delete(int id);
        (bool, string) Update(int id, UpdateTrainerVM curr);
        Task<(bool, string)> Create(AddTrainerVM newmember);

       (bool,string, IEnumerable<GetSessionVM>?) GetTrainerSessions(int trainerId);
    }
}
