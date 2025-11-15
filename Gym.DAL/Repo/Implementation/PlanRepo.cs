
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;

namespace Gym.DAL.Repo.Implementation
{
    public class PlanRepo : IPlanRepo
    {
        private readonly GymDbContext _context;

        public PlanRepo(GymDbContext context)
        {
            _context = context;
        }

        public bool AddSessionToPlan(int planId, int sessionId)
        {
            try
            {
                var plan = _context.plans.FirstOrDefault(p => p.Id == planId);
                var session = _context.sessions.FirstOrDefault(s => s.Id == sessionId);
                if (plan == null || session == null)
                {
                    throw new Exception("Plan or Session not found.");
                }
                plan.Sessions.Add(session);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CancelMemberPlan(int memberPlanId)
        {
            try
            {
                var memberPlan = _context.Set<MemberPlan>().FirstOrDefault(mp => mp.Id == memberPlanId);
                if (memberPlan == null)
                {
                    throw new Exception("MemberPlan not found.");
                }
                memberPlan.GetType().GetProperty("IsActive")?.SetValue(memberPlan, false);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CountMembersInPlan(int planId)
        {
            try
            {
                var count = _context.Set<MemberPlan>().Count(mp => mp.PlanId == planId && mp.IsActive);
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CreatePlan(Plan plan)
        {
            try
            {
                var result = _context.plans.Add(plan);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeletePlan(int id)
        {
            try
            {
                var plan = _context.plans.FirstOrDefault(p => p.Id == id);
                if (plan == null)
                {
                    throw new Exception("Plan not found.");
                }
                _context.plans.Remove(plan);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Plan> GetAllPlans()
        {
            try
            {
                var plans = _context.plans.ToList();
                return plans;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Plan? GetPlanById(int id)
        {
            try
            {
                var plan = _context.plans.FirstOrDefault(p => p.Id == id);
                return plan;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<MemberPlan> GetPlanMembers(int planId)
        {
            try
            {
                var members = _context.Set<MemberPlan>().Where(mp => mp.PlanId == planId).ToList();
                return members;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Session> GetPlanSessions(int planId)
        {
            try
            {
                var plan = _context.plans.FirstOrDefault(p => p.Id == planId);
                if (plan == null)
                {
                    throw new Exception("Plan not found.");
                }
                return plan.Sessions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool MemberAlreadySubscribed(int memberId, int planId)
        {
            try
            {
                var exists = _context.Set<MemberPlan>().Any(mp => mp.MemberId == memberId && mp.PlanId == planId && mp.IsActive);
                return exists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PlanExists(int planId)
        {
            try
            {
                var exists = _context.plans.Any(p => p.Id == planId);
                return exists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveSessionFromPlan(int planId, int sessionId)
        {
            try
            {
                var plan = _context.plans.FirstOrDefault(p => p.Id == planId);
                var session = _context.sessions.FirstOrDefault(s => s.Id == sessionId);
                if (plan == null || session == null)
                {
                    throw new Exception("Plan or Session not found.");
                }
                plan.Sessions.Remove(session);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool SubscribeMemberToPlan(MemberPlan subscription)
        {
            try
            {
                var result = _context.Set<MemberPlan>().Add(subscription);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdatePlan(Plan plan)
        {
            try {
                var existingPlan = _context.plans.FirstOrDefault(p => p.Id == plan.Id);
                if (existingPlan == null)
                {
                    throw new Exception("Plan not found.");
                }
                existingPlan.Update(plan);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
