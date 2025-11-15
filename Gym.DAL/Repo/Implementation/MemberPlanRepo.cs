
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;

namespace Gym.DAL.Repo.Implementation
{
    public class MemberPlanRepo : IMemberPlanRepo
    {
        private readonly GymDbContext _context;

        public MemberPlanRepo(GymDbContext context)
        {
            _context = context;
        }

        public bool ActivateSubscription(int memberPlanId)
        {
            try
            {
                var memberPlan = _context.Set<MemberPlan>().FirstOrDefault(mp => mp.Id == memberPlanId);
                if (memberPlan == null)
                {
                    throw new Exception("MemberPlan not found.");
                }
                memberPlan.Active();
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CancelSubscription(int memberPlanId)
        {
            try
            {
                var memberPlan = _context.Set<MemberPlan>().FirstOrDefault(mp => mp.Id == memberPlanId);
                if (memberPlan == null)
                {
                    throw new Exception("MemberPlan not found.");
                }
                memberPlan.DeActive();
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Create(MemberPlan memberPlan)
        {
            try
            {
                var existingMemberPlan = _context.Set<MemberPlan>()
                    .FirstOrDefault(mp => mp.MemberId == memberPlan.MemberId && mp.PlanId == memberPlan.PlanId && mp.IsActive);
                if (existingMemberPlan != null)
                {
                    throw new Exception("Member is already subscribed to this plan.");
                }
                _context.Set<MemberPlan>().Add(memberPlan);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var memberPlan = _context.Set<MemberPlan>().FirstOrDefault(mp => mp.Id == id);
                if (memberPlan == null)
                {
                    throw new Exception("MemberPlan not found.");
                }
                _context.Set<MemberPlan>().Remove(memberPlan);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MemberPlan? GetActivePlanForMember(int memberId)
        {
            try
            {
                var memberPlan = _context.Set<MemberPlan>()
                    .FirstOrDefault(mp => mp.MemberId == memberId && mp.IsActive);
                return memberPlan;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<MemberPlan> GetAll()
        {
            try
            {
                var memberPlans = _context.Set<MemberPlan>().ToList();
                return memberPlans;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MemberPlan? GetById(int id)
        {
            try
            {
                var memberPlan = _context.Set<MemberPlan>().FirstOrDefault(mp => mp.Id == id);
                return memberPlan;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<MemberPlan> GetMemberPlans(int memberId)
        {
            try
            {
                var memberPlans = _context.Set<MemberPlan>()
                    .Where(mp => mp.MemberId == memberId)
                    .ToList();
                return memberPlans;
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

        public bool HasActivePlan(int memberId)
        {
            try
            {
                var hasActivePlan = _context.Set<MemberPlan>()
                    .Any(mp => mp.MemberId == memberId && mp.IsActive);
                return hasActivePlan;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsMemberSubscribedToPlan(int memberId, int planId)
        {
            try
            {
                var isSubscribed = _context.Set<MemberPlan>()
                    .Any(mp => mp.MemberId == memberId && mp.PlanId == planId && mp.IsActive);
                return isSubscribed;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Update(MemberPlan memberPlan)
        {
            try
            {
                var existingMemberPlan = _context.Set<MemberPlan>().FirstOrDefault(mp => mp.Id == memberPlan.Id);
                if (existingMemberPlan == null)
                {
                    throw new Exception("MemberPlan not found.");
                }
                existingMemberPlan.Update(memberPlan);
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
