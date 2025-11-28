using Gym.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.Repo.Abstraction
{
    public interface ISessionRepo
    {
        (bool, string?) AddSession(Session session);
        (bool, string?) Update(Session session);
        (bool, string?) Delete(int id);
        (bool, Session?) GetById(int id);
        (bool, IEnumerable<Session>?) GetAll();

        // Trainer-related
        (bool,IEnumerable<Session>?) GetSessionsByTrainerId(int trainerId);

        (bool,IEnumerable<Session>?) GetUpcomingSessions();
        (bool,IEnumerable<Session>?) GetOnGoingSessions();
        (bool,IEnumerable<Session>?) GetPastSessions();

    }
}
