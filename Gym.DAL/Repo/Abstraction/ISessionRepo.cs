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
        (bool, string?) UpdateSession(Session session);
        (bool, string?) DeleteSession(int id);
        (bool,Session?) GetSessionById(int id);
        (bool,IEnumerable<Session>?) GetAllSession();


        // Trainer Related
        (bool,IEnumerable<Session>?) GetSessionsByTrainerId(int trainerId);


        (bool,IEnumerable<Session>?) GetUpcomingSessions();
        (bool,IEnumerable<Session>) GetPastSessions();
    }
}
