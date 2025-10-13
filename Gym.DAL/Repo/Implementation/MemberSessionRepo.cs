using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;


namespace Gym.DAL.Repo.Implementation
{
    public class MemberSessionRepo : IMemberSessionRepo
    {
        private readonly GymDbContext GymDb;
        public MemberSessionRepo(GymDbContext GymDb)
        {
            this.GymDb = GymDb;
        }

        public (bool, string) AddMemberToSession(int memberId, int sessionId)
        {
            try
            {
                var member = GymDb.members.FirstOrDefault(a => a.Id == memberId.ToString());
                if (member == null) return (false, "member is not found");

                var session = GymDb.sessions.FirstOrDefault(a => a.Id == sessionId);
                if (session == null) return (false, "session is not found");
                var trainer = GymDb.trainers.FirstOrDefault(a => a.Id == session.TrainerId.ToString());   

                bool alreadyExists = GymDb.memberSessions
                        .Any(ms => ms.memberId == memberId.ToString() && ms.sessionId == sessionId);
                if (alreadyExists) return (false, "already exist");

                var newMember = new MemberSession(memberId.ToString(), sessionId);
                GymDb.memberSessions.Add(newMember);
                GymDb.SaveChanges();
                return (true, "your registeration is done");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string, IEnumerable<Member>?) GetMembersBySession(int sessionId)
        {
            try
            {
                var session = GymDb.sessions.Any(a => a.Id == sessionId);
                if (!session) return (false, "session is not found", null);

                var members = GymDb.memberSessions
                    .Where(ms => ms.sessionId == sessionId)
                    .Select(ms => ms._Member)
                    .ToList();
                if (!members.Any()) return (false, "session is empty", null);

                return (true, "done", members);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, IEnumerable<Session>?) GetSessionsByMember(int memberId)
        {
            try
            {
                var member = GymDb.members.Any(a => a.Id == memberId.ToString());
                if (!member) return (false, "member is not found", null);

                var sessions = GymDb.memberSessions
                    .Where(a => a.memberId == memberId.ToString())
                    .Select(a => a._Session).ToList();
                if (!sessions.Any()) return (false, "member havn't sessions", null);

                return (true, "done", sessions);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string) RemoveAllMembersFromSession(int sessionId)
        {
            try
            {
                var session = GymDb.sessions.FirstOrDefault(a => a.Id == sessionId);
                if (session == null) return (false, "session is not found");

                var memberSessions = GymDb.memberSessions
                   .Where(ms => ms.sessionId == sessionId).ToList();

                if (!memberSessions.Any())
                    return (false, "session is empty");
                GymDb.memberSessions.RemoveRange(memberSessions);
       
                GymDb.SaveChanges();

                return (true, "done");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string) RemoveAllSessionsForMember(int memberId)
        {
            try
            {
                var memberExists = GymDb.members.Any(m => m.Id == memberId.ToString());
                if (!memberExists) return (false, "member is not found");

                var memberSessions = GymDb.memberSessions
                    .Where(ms => ms.memberId == memberId.ToString())
                    .ToList();

                if (!memberSessions.Any()) return (false, "member havn't any sessions");


                GymDb.memberSessions.RemoveRange(memberSessions);
                GymDb.SaveChanges();
                return (true, "done");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string) RemoveMemberFromSession(int memberId, int sessionId)
        {
            try
            {
                var member = GymDb.members.FirstOrDefault(a => a.Id == memberId.ToString());
                if (member == null) return (false, "member is not found");

                var session = GymDb.sessions.FirstOrDefault(a => a.Id == sessionId);
                if (session == null) return (false, "session is not found");

                var membersession = GymDb.memberSessions
                    .FirstOrDefault(ms => ms.memberId == memberId.ToString() && ms.sessionId == sessionId);
                if (membersession == null) return (false, "member is not register in this session");

                GymDb.memberSessions.Remove(membersession);
                GymDb.SaveChanges();
                return (true, "done");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
