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
                var result = GymDb.sessions.Add(session);
                GymDb.SaveChanges();
                if(result.Entity.Id == 0)
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

        public (bool, string?) DeleteSession(int id)
        {
            try
            {
                var session = GymDb.sessions.FirstOrDefault(s => s.Id == id);
                if (session == null)
                    return (false, "This session not found.");
                GymDb.sessions.Remove(session);
                GymDb.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, "Error deleting this session.");
            }
        }

        public (bool, IEnumerable<Session>?) GetAllSession()
        {
            try
            {
                var AllSessions = GymDb.sessions.ToList();
                if(!AllSessions.Any())
                {
                    return(false, null);
                }
                return(true, AllSessions);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public (bool,IEnumerable<Session>?) GetPastSessions()
        {
            try
            {
                var PastSessions = GymDb.sessions.Where(a => a.ScheduleTime < DateTime.Now).ToList();
                if(!PastSessions.Any())
                {
                    return (false, null);
                }
                return(true, PastSessions);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public (bool, Session?) GetSessionById(int id)
        {
            try
            {
                var session = GymDb.sessions.FirstOrDefault(a => a.Id == id);
                if (session == null)
                    return (false, null);
                return (true, session);
            }
            catch(Exception ex)
            {
                return(false, null);
            }
        }

        public (bool,IEnumerable<Session>?) GetSessionsByTrainerId(int trainerId)
        {
            try
            {
                var TrainerSession = GymDb.sessions.Where(a => a.TrainerId == trainerId).ToList();
                if(!TrainerSession.Any())
                    return(false, null);
                return(true, TrainerSession);
            }
            catch(Exception ex)
            { 
                return(false, null); 
            }
        }

        public (bool,IEnumerable<Session>?) GetUpcomingSessions()
        {
            try
            {
                var UpcomingSessions = GymDb.sessions.Where(a => a.ScheduleTime >= DateTime.Now).ToList();
                if (!UpcomingSessions.Any())
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


        public (bool, string?) UpdateSession(Session session)
        {
            try
            {
                var OldSession = GymDb.sessions.Where(a => a.Id ==  session.Id).FirstOrDefault();
                if(OldSession == null) return (false, "This session not found.");
                if (OldSession.Update(session))
                {
                    GymDb.SaveChanges();
                    return (true, null);
                }
                return(false, "Some thig goes wrong.");
            }
            catch(Exception ex)
            {
                return(false, "Error in Updating operation.");
            }
        }
    }
}
