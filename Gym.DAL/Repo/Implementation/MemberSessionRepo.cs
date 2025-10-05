using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.Repo.Implementation
{
    public class MemberSessionRepo : IMemberSessionRepo
    {
        private readonly GymDbContext GymDb;
        public MemberSessionRepo(GymDbContext GymDb)
        {
            this.GymDb = GymDb;
        }

        public bool AddMemberToSession(int memberId, int sessionId)
        {
            try
            {
                var member = GymDb.members.FirstOrDefault(a => a.Id ==  memberId);
                if (member == null) return false;

                var session = GymDb.sessions.FirstOrDefault(a => a.Id == sessionId);
                if (session == null) return false;

                bool alreadyExists = GymDb.memberSessions
                        .Any(ms => ms.memberId == memberId && ms.sessionId == sessionId);
                if (alreadyExists) return false;

                var newMember = new MemberSession(memberId, sessionId);
                GymDb.memberSessions.Add(newMember);
                GymDb.SaveChanges();
                return true;
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public (bool,IEnumerable<Member>?) GetMembersBySession(int sessionId)
        {
            try
            {
                var session = GymDb.sessions.Any(a => a.Id == sessionId);
                if(!session) return (false, null);
                
                var members = GymDb.memberSessions
                    .Where(ms => ms.sessionId == sessionId)
                    .Select(ms => ms._Member) 
                    .ToList();
                if (!members.Any()) return (false, null);

                return (true, members);
            }
            catch (Exception ex)
            {
                return(false,null);
            }
        }

        public (bool,IEnumerable<Session>?) GetSessionsByMember(int memberId)
        {
            try
            {
                var member = GymDb.members.Any(a => a.Id == memberId);
                if(!member) return(false,null);

                var sessions = GymDb.memberSessions
                    .Where(a => a.memberId  == memberId)
                    .Select(a => a._Session) .ToList();
                if(!sessions.Any()) return(false,null);
                
                return (true, sessions);
            }
            catch(Exception ex)
            {
                return(false,null);
            }
        }


        public bool RemoveAllMembersFromSession(int sessionId)
        {
            try
            {
                var session = GymDb.sessions.Any(a => a.Id == sessionId);
                if (!session) return false;

                var memberSessions = GymDb.memberSessions
                   .Where(ms => ms.sessionId == sessionId).ToList();

                if (!memberSessions.Any())
                    return false;
                GymDb.memberSessions.RemoveRange(memberSessions);
                GymDb.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool RemoveAllSessionsForMember(int memberId)
        {
            try
            {
                var memberExists = GymDb.members.Any(m => m.Id == memberId);
                if (!memberExists) return false;

                var memberSessions = GymDb.memberSessions
                    .Where(ms => ms.memberId == memberId)
                    .ToList();

                if (!memberSessions.Any()) return false;

                GymDb.memberSessions.RemoveRange(memberSessions);
                GymDb.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveMemberFromSession(int memberId, int sessionId)
        {
            try
            {
                var member = GymDb.members.FirstOrDefault(a => a.Id == memberId);
                if (member == null) return false;

                var session = GymDb.sessions.FirstOrDefault(a => a.Id == sessionId);
                if (session == null) return false;

                var membersession = GymDb.memberSessions
                    .FirstOrDefault(ms => ms.memberId == memberId && ms.sessionId == sessionId);
                if (membersession == null) return false;

                GymDb.memberSessions.Remove(membersession);
                GymDb.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
