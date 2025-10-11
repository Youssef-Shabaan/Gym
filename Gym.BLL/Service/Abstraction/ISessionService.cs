
using Gym.BLL.ModelVM.Session;
using Gym.DAL.Entities;

namespace Gym.BLL.Service.Abstraction
{
    public interface ISessionService
    {
        (bool, string?) AddSession(AddUpdateSessionVM session);
        (bool, string?) Update(AddUpdateSessionVM session);
        (bool, string?) Delete(int id);
        (bool, GetSessionVM?) GetById(int id);
        (bool, string, IEnumerable<GetSessionVM>?) GetAll();

        (bool, string, IEnumerable<GetSessionVM>?) GetSessionsByTrainerId(int trainerId);

        (bool, string, IEnumerable<GetSessionVM>?) GetUpcomingSessions();
        (bool, string, IEnumerable<GetSessionVM>?) GetPastSessions();
    }
}
