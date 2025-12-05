using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Gym.DAL.Repo.Implementation
{
    public class SessionRepo : ISessionRepo
    {
        private readonly GymDbContext GymDb;
        public SessionRepo(GymDbContext GymDb)
        {
            this.GymDb = GymDb;
        }
        public (bool, string?) AddSession(Session session)
        {
            try
            {
                bool hasConflict = GymDb.sessions.Any(s =>
                s.TrainerId == session.TrainerId &&
                (
                    (session.StartTime >= s.StartTime && session.StartTime < s.EndTime) ||
                    (session.EndTime > s.StartTime && session.EndTime <= s.EndTime) ||
                    (session.StartTime <= s.StartTime && session.EndTime >= s.EndTime)
                ));

                if (hasConflict)
                {
                    return (false, "Trainer already has another session at this time.");
                }

                var result = GymDb.sessions.Add(session);
                GymDb.SaveChanges();

                if (result.Entity.Id == 0)
                {
                    return (false, "Error adding this session.");
                }

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, "Error adding this session.");
            }
        }

        public (bool, string?) Delete(int id)
        {
            try
            {
                var session = GymDb.sessions.Where(a => a.Id == id).FirstOrDefault();
                if (session == null)
                {
                    return (false, "This session not found.");
                }
                GymDb.sessions.Remove(session);
                GymDb.SaveChanges();
                return (true, null);
            }
            catch(Exception ex)
            {
                return(false, ex.Message);
            }
        }

        public (bool, IEnumerable<Session>?) GetAll()
        {
            try
            {
                var sessions = GymDb.sessions.Where(s=>s.PlanId==null).Include(t => t._Trainer).ThenInclude(u => u.User).ToList();
                if(!sessions.Any())
                {
                    return(false, null);
                }
                return(true, sessions);
            }
            catch(Exception ex)
            {
                return (false, null);
            }
        }

        public (bool, Session?) GetById(int id)
        {
            try
            {
                var session = GymDb.sessions.Include(t => t._Trainer).ThenInclude(u => u.User)
                    .Where(a =>a.Id == id).FirstOrDefault();  
                
                if(session == null)
                {
                    return (false, null);
                }
                return(true, session);
            }
            catch(Exception ex)
            {
                return (false, null);
            }
        }

        public (bool, IEnumerable<Session>?) GetOnGoingSessions()
        {
            try
            {
                var OnGoingSessions = GymDb.sessions
                    .Include(t => t._Trainer).ThenInclude(u => u.User)
                    .Where(a => DateTime.Now >= a.StartTime && DateTime.Now <= a.EndTime).ToList();
                if (!OnGoingSessions.Any() || OnGoingSessions == null)
                {
                    return (false, null);
                }
                return (true, OnGoingSessions);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public (bool, IEnumerable<Session>?) GetPastSessions()
        {
            try
            {
                var PastSessions = GymDb.sessions
                    .Include(t => t._Trainer).ThenInclude(u => u.User)
                    .Where(a => a.EndTime <  DateTime.Now).OrderByDescending(s => s.StartTime).Take(20).ToList();
                if(!PastSessions.Any() || PastSessions == null)
                {
                    return(false, null);
                }
                return(true, PastSessions);
            }
            catch (Exception ex)
            {
                return(false, null);
            }
        }

        public (bool, IEnumerable<Session>?) GetSessionsByTrainerId(int trainerId)
        {
            try
            {
                var sessions = GymDb.sessions
                    .Include(t => t._Trainer).ThenInclude(u => u.User)
                    .Where(a => a.TrainerId == trainerId).ToList();
                if(!sessions.Any() || sessions == null)
                {
                    return(false, null);
                }
                return(true, sessions);
            }
            catch (Exception ex)
            {
                return(false, null);
            }
        }

        public (bool, IEnumerable<Session>?) GetUpcomingSessions()
        {
            try
            {
                var UpcomingSessions = GymDb.sessions
                    .Include(t => t._Trainer).ThenInclude(u => u.User)
                    .Where(a => a.StartTime >= DateTime.Now).ToList();
                if (!UpcomingSessions.Any() || UpcomingSessions == null)
                {
                    return (false, null);
                }
                return (true, UpcomingSessions);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public int SessionsCount()
        {
            return GymDb.sessions.Count();
        }

        public (bool, string?) Update(Session session)
        {
            try
            {
                var OldSession = GymDb.sessions.FirstOrDefault(a => a.Id == session.Id);
                if(OldSession == null)
                {
                    return (false, "This session not found.");
                }
                bool hasConflict = GymDb.sessions.Any(a => a.TrainerId ==  session.TrainerId && a.Id != session.Id &&
                (
                    (session.StartTime >= a.StartTime && session.StartTime < a.EndTime) ||
                    (session.EndTime > a.StartTime && session.EndTime <= a.EndTime) ||
                    (session.StartTime <= a.StartTime && session.EndTime >= a.EndTime)
                ));
                if (hasConflict)
                {
                    return (false, "Trainer already has another session at this time.");
                }
                OldSession.Update(session);
                GymDb.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return(false, ex.Message);
            }
        }
        public (bool, string, List<Session>) GetSessionforPlan(int planid)
        {
            try {
                var sessions = GymDb.sessions.Where(s => s.PlanId == planid).ToList();
                return (true, "retrieved", sessions);
            }
            catch (Exception ex) { 
                return (false, ex.Message, null);
            }
        }
    }
}
