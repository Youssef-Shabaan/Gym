using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Gym.DAL.Repo.Implementation
{
    public class MemberSessionRepo : IMemberSessionRepo
    {
        private readonly GymDbContext GymDb;
        public MemberSessionRepo(GymDbContext GymDb)
        {
            this.GymDb = GymDb;
        }

        public (bool, string) Add(MemberSession memberSession)
        {
            try
            {
                var member = GymDb.members.FirstOrDefault(m => m.MemberId == memberSession.MemberId);
                if (member == null)
                {
                    return (false, "Member not found.");
                }
                var session = GymDb.sessions.FirstOrDefault(s => s.Id == memberSession.SessionId);
                if (session == null)
                {
                    return (false, "Session not found.");
                }
                bool BookSession = session.Book();
                if (!BookSession)
                {
                    return (false, "All seats have been booked.");
                }
                var result = GymDb.memberSessions.Add(memberSession);
                GymDb.SaveChanges();
                return (true, "Added member to session is done");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string) Delete(int id)
        {
            try
            {
                var memberSession = GymDb.memberSessions.FirstOrDefault(a => a.Id == id);
                if (memberSession == null)
                {
                    return (false, "Member session not found");
                }
                var session = GymDb.sessions.FirstOrDefault(s => s.Id == memberSession.SessionId);

                session.Cancel();

                var result = GymDb.memberSessions.Remove(memberSession);
                GymDb.SaveChanges();
                return (true, "Removed Successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string, IEnumerable<MemberSession>?) GetAll()
        {
            try
            {
                var memberSession = GymDb.memberSessions
                    .Include(p => p.Payment)
                    .Include(m => m.Member).ThenInclude(u => u.User)
                    .Include(s => s.Session).ThenInclude(t => t._Trainer).ThenInclude(u => u.User)
                    .ToList();
                if (!memberSession.Any())
                {
                    return (false, "Empty", null);
                }
                return (true, null, memberSession);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, MemberSession?) GetById(int Id)
        {
            try
            {
                var memberSession = GymDb.memberSessions
                    .Include(p => p.Payment)
                    .Include(m => m.Member).ThenInclude(u => u.User)
                    .Include(s => s.Session).ThenInclude(t => t._Trainer).ThenInclude(u => u.User)
                    .FirstOrDefault(a => a.Id == Id);

                if (memberSession == null)
                {
                    return (true, "Member session is not found", null);
                }
                return (true, null, memberSession);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, IEnumerable<MemberSession>?) GetByMemberId(int MemberId)
        {
            try
            {
                var memberSession = GymDb.memberSessions
                    .Include(p => p.Payment)
                    .Include(m => m.Member).ThenInclude(u => u.User)
                    .Include(s => s.Session).ThenInclude(t => t._Trainer).ThenInclude(u => u.User)
                    .Where(a => a.MemberId == MemberId).ToList();
                if (!memberSession.Any())
                {
                    return (false, "Empty", null);
                }
                return (true, null, memberSession);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, IEnumerable<MemberSession>?) GetBySessionId(int SessionId)
        {
            try
            {
                var memberSession = GymDb.memberSessions
                    .Include(p => p.Payment)
                    .Include(m => m.Member).ThenInclude(u => u.User)
                    .Include(s => s.Session).ThenInclude(t => t._Trainer).ThenInclude(u => u.User)
                    .Where(a => a.SessionId == SessionId).ToList();
                if (!memberSession.Any())
                {
                    return (false, "Empty", null);
                }
                return (true, null, memberSession);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public Member GetMemberByUserId(string userid)
        {
            try
            {
                var member = GymDb.members
                    .Include(u => u.User)
                    .FirstOrDefault(m => m.User.Id == userid);
                return member;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public (bool, string, IEnumerable<MemberSession>) GetMembersForSession(int sessionId)
        {
            try
            {
                var session = GymDb.sessions.FirstOrDefault(s => s.Id == sessionId);
                if(session == null)
                {
                    return (false, "Session not found", null);
                }
                var membersSession = GymDb.memberSessions.Include(ms => ms.Member)
                    .ThenInclude(m => m.User)
                    .Where(s => s.SessionId == sessionId).ToList();

                return (true, null, membersSession);   
            }
            catch(Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public Session GetSessionById(int sessionId)
        {
            try
            {
                var session = GymDb.sessions
                    .FirstOrDefault(s => s.Id == sessionId);
                return session;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool IsMemberBooked(int memberId, int sessionId)
        {
            try
            {
                var memberSession = GymDb.memberSessions
                    .FirstOrDefault(ms => ms.MemberId == memberId && ms.SessionId == sessionId);
                if (memberSession == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public (bool, string) SetAttendance(int memberId, int sessionId)
        {
            try
            {
                var member = GymDb.members.FirstOrDefault(m => m.MemberId == memberId);
                if (member == null)
                {
                    return (false, "This member not found");
                }
                var session = GymDb.sessions.FirstOrDefault(s => s.Id == sessionId);
                if (session == null)
                {
                    return (false, "This session not found");
                }

                var memberSession = GymDb.memberSessions.FirstOrDefault(ms => ms.MemberId == memberId && ms.SessionId == sessionId);
                if (memberSession == null)
                {
                    return (false, "Member session not found");
                }

                if (memberSession.IsAttended == false)
                {
                    memberSession.IsAttended = true;
                    memberSession.Status = "Attended";
                    GymDb.SaveChanges();
                    return (true, "Attendance marked successfully");
                }
                // else
                memberSession.IsAttended = false;
                memberSession.Status = "Absent";
                GymDb.SaveChanges();
                return (true, "Attendance removed successfully");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string) Update(MemberSession memberSession)
        {
            try
            {
                var oldMemberSession = GymDb.memberSessions.FirstOrDefault(m => m.Id == memberSession.Id);
                if (oldMemberSession == null)
                {
                    return (false, "This member session not found");
                }
                bool result = oldMemberSession.Update(memberSession);
                GymDb.SaveChanges();
                return (result, "Updated Successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public int GetSessionId(int memberSessionId)
        {
            var membersession = GymDb.memberSessions.FirstOrDefault(s => s.Id == memberSessionId);
            if(membersession == null)
            {
                return 0;
            }
            return membersession.SessionId;
        }

    }
}
