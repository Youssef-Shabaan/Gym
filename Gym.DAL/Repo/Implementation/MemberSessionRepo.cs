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
                    .Include(m => m.Member)
                    .Include(s => s.Session)
                    .Include(t => t.TrainerSubscription)
                    .ToList();
                if(!memberSession.Any())
                {
                    return (false, "Empty", null);
                }
                return (true, null, memberSession);
            }
            catch (Exception ex)
            {
                return(false, ex.Message, null);
            }
        }

        public (bool, string, MemberSession?) GetById(int Id)
        {
            try
            {
                var memberSession = GymDb.memberSessions
                    .Include(p => p.Payment)
                    .Include(m => m.Member)
                    .Include(s => s.Session)
                    .Include(t => t.TrainerSubscription)
                    .FirstOrDefault(a => a.Id == Id);

                if (memberSession == null)
                {
                    return(true, "Member session is not found", null);
                }
                return (true, null, memberSession);
            }
            catch (Exception ex)
            {
                return(false, ex.Message, null);
            }
        }

        public (bool, string, IEnumerable<MemberSession>?) GetByMemberId(int MemberId)
        {
            try
            {
                var memberSession = GymDb.memberSessions
                    .Include(p => p.Payment)
                    .Include(m => m.Member)
                    .Include(s => s.Session)
                    .Include(t => t.TrainerSubscription)
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
                    .Include(m => m.Member)
                    .Include(s => s.Session)
                    .Include(t => t.TrainerSubscription)
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

        public (bool, string, IEnumerable<MemberSession>?) GetByTrainerSubscriptionId(int trainerSubscriptionId)
        {
            try
            {
                var memberSession = GymDb.memberSessions
                    .Include(p => p.Payment)
                    .Include(m => m.Member)
                    .Include(s => s.Session)
                    .Include(t => t.TrainerSubscription)
                    .Where(a => a.TrainerSubscriptionId == trainerSubscriptionId).ToList();
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

        public (bool, string) Update(MemberSession memberSession)
        {
            try
            {
                var oldMemberSession = GymDb.memberSessions.FirstOrDefault(m => m.Id == memberSession.Id);
                if(oldMemberSession == null)
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
    }
}
