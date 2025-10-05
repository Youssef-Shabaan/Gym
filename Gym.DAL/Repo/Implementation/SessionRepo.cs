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
                return(true, null);
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
                var sessions = GymDb.sessions.ToList();
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
                var session = GymDb.sessions.Where(a =>a.Id == id).FirstOrDefault();    
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

        public (bool, IEnumerable<Session>?) GetPastSessions()
        {
            try
            {
                var PastSessions = GymDb.sessions.Where(a => a.ScheduleTime <  DateTime.Now).ToList();
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
                var sessions = GymDb.sessions.Where(a => a.TrainerId == trainerId).ToList();
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
                var UpcomingSessions = GymDb.sessions.Where(a => a.ScheduleTime >= DateTime.Now).ToList();
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

        public (bool, string?) Update(Session session)
        {
            try
            {
                var OldSession = GymDb.sessions.Where(a => a.Id == session.Id).FirstOrDefault();
                if(OldSession == null)
                {
                    return (false, "This session not found.");
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
    }
}
