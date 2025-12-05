
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Gym.DAL.Repo.Implementation
{
    public class MemberPlanRepo : IMemberPlanRepo
    {
        private readonly GymDbContext _context;

        public MemberPlanRepo(GymDbContext context)
        {
            _context = context;
        }

        public (bool ,string) ActivateSubscription(int memberPlanId)
        {
            try
            {
                var memberPlan = _context.memberPlans.FirstOrDefault(mp => mp.Id == memberPlanId);
                if (memberPlan == null)
                {
                    return(false, "MemberPlan not found.");
                }
                if(memberPlan.IsActive)
                {
                    return (false, "MemeberPlan already active.");
                }
                memberPlan.Active();
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string) CancelSubscription(int memberPlanId)
        {
            try
            {
                var memberPlan = _context.memberPlans.FirstOrDefault(mp => mp.Id == memberPlanId);
                if (memberPlan == null)
                {
                    return(false, "MemberPlan not found.");
                }
                if (!memberPlan.IsActive)
                {
                    return (false, "MemberPlan already not active.");
                }
                memberPlan.DeActive();
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string) Create(MemberPlan memberPlan)
        {
            try
            {
                var existingMemberPlan = _context.memberPlans
                    .FirstOrDefault(mp => mp.MemberId == memberPlan.MemberId && mp.PlanId == memberPlan.PlanId && mp.IsActive);
                if (existingMemberPlan != null)
                {
                    return(false, "Member is already subscribed to this plan.");
                }
                _context.memberPlans.Add(memberPlan);
                _context.SaveChanges();
                return (true, "Member added to plan successfully.");
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
                var memberPlan = _context.memberPlans.FirstOrDefault(mp => mp.Id == id);
                if (memberPlan == null)
                {
                    return(false, "MemberPlan not found.");
                }
                _context.Set<MemberPlan>().Remove(memberPlan);
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return(false, ex.Message);
            }
        }

        public (bool, string, IEnumerable<MemberPlan>?) GetActivePlanForMember(int memberId)
        {
            try
            {
                var memberPlan = _context.memberPlans
                    .Include(m => m.Member).ThenInclude(u => u.User)
                    .Include(p => p.Plan)
                    .Where(mp => mp.MemberId == memberId && mp.IsActive).ToList();
                if(!memberPlan.Any())
                {
                    return (false, "There are no active plans", null);
                }
                return (true, null, memberPlan);
            }
            catch (Exception ex)
            {
                return(false, ex.Message, null);    
            }
        }

        public (bool, string, IEnumerable<MemberPlan>?) GetAll()
        {
            try
            {
                var memberPlans = _context.memberPlans
                    .Include(m => m.Member).ThenInclude(u => u.User)
                    .Include(p => p.Plan).ToList();
                if(!memberPlans.Any())
                {
                    return (false, "There are no plans.", null);
                }
                return (true, null, memberPlans);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, MemberPlan?) GetById(int id)
        {
            try
            {
                var memberPlan = _context.memberPlans
                    .Include(m => m.Member).ThenInclude(u => u.User)
                    .Include(p => p.Plan)
                    .FirstOrDefault(mp => mp.Id == id);
                if(memberPlan == null)
                {
                    return (false, null);
                }
                return (true, memberPlan);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public (bool, string, IEnumerable<MemberPlan>?) GetMemberPlans(int memberId)
        {
            try
            {
                var memberPlans = _context.memberPlans
                    .Include(m => m.Member).ThenInclude(u => u.User)
                    .Include(p => p.Plan)
                    .Where(mp => mp.MemberId == memberId)
                    .ToList();
                if(!memberPlans.Any())
                {
                    return (false, "There are no plans for this member.", null);
                }
                return(true, null, memberPlans);
            }
            catch (Exception ex)
            {
                return(false, ex.Message, null);
            }
        }

        

        public bool HasActivePlan(int memberId)
        {
            try
            {
                var hasActivePlan = _context.memberPlans
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
                var isSubscribed = _context.memberPlans
                    .Any(mp => mp.MemberId == memberId && mp.PlanId == planId);
                return isSubscribed;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public (bool, string) Update(MemberPlan memberPlan)
        {
            try
            {
                var existingMemberPlan = _context.memberPlans.FirstOrDefault(mp => mp.Id == memberPlan.Id);
                if (existingMemberPlan == null)
                {
                    return(false, "MemberPlan not found.");
                }
                existingMemberPlan.Update(memberPlan);
                _context.SaveChanges();
                return (true, "Updated successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public (bool, string, List<MemberPlan>) GetMembersForPlan(int planid)
        {
            try { 
                var plan = _context.plans.FirstOrDefault(p => p.Id == planid);
                if (plan == null)
                {
                    return (false, "Plan not found", null);
                }
                var memberPlans = _context.memberPlans
                    .Include(mp => mp.Member).ThenInclude(m => m.User)
                    .Where(mp => mp.PlanId == planid)
                    .ToList();
                
                return (true, null, memberPlans);
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
                var member = _context.members
                    .Include(u => u.User)
                    .FirstOrDefault(m => m.User.Id == userid);
                return member;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Plan GetPlanById(int planId)
        {
            try
            {
                var plan = _context.plans
                    .FirstOrDefault(s => s.Id == planId);
                return plan;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool IsMemberBooked(int memberId, int planId)
        {
            try
            {
                var memberPlan = _context.memberPlans
                    .FirstOrDefault(ms => ms.MemberId == memberId && ms.PlanId == planId);
                if (memberPlan == null)
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
    }
}
