
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Gym.DAL.Repo.Implementation
{
    public class PlanRepo : IPlanRepo
    {
        private readonly GymDbContext _context;

        public PlanRepo(GymDbContext context)
        {
            _context = context;
        }

        public (bool, string) CreatePlan(Plan plan)
        {
            try
            {
                var result = _context.plans.Add(plan);
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public (bool, string) DeletePlan(int id)
        {
            try
            {
                var plan = _context.plans.FirstOrDefault(p => p.Id == id);
                if (plan == null)
                {
                    return (false, "Plan not found.");
                }
                _context.plans.Remove(plan);
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public bool AddSessionToPlan(int planId, int sessionId)
        {
            try
            {
                var plan = _context.plans.Include(p => p.Sessions).FirstOrDefault(p => p.Id == planId);
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

        public (bool, string, IEnumerable<Plan>) GetAllPlans()
        {
            try
            {
                var plans = _context.plans.Include(t => t.Trainer).Include(s => s.Sessions).ToList();
                if (!plans.Any())
                {
                    return (false, "There are no plans available", null);
                }
                return (true, null, plans);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, Plan?) GetPlanById(int id)
        {
            try
            {
                var plan = _context.plans.Include(t => t.Trainer).Include(s => s.Sessions).FirstOrDefault(p => p.Id == id);
                if (plan == null)
                {
                    return (false, null);
                }
                return (true, plan);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public (bool, string, IEnumerable<Session>) GetPlanSessions(int planId)
        {
            try
            {
                var plan = _context.plans.Include(p => p.Sessions).FirstOrDefault(p => p.Id == planId);
                if (plan == null)
                    return (false, "Plan not found.", null);

                var sessions = plan.Sessions.ToList();
                return (true, null, sessions);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
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
                return false;
            }
        }

        public (bool, string) RemoveSessionFromPlan(int planId, int sessionId)
        {
            try
            {
                var plan = _context.plans.FirstOrDefault(p => p.Id == planId);
                var session = _context.sessions.FirstOrDefault(s => s.Id == sessionId);
                if (plan == null || session == null)
                {
                    return (false, "Plan or Session not found.");
                }
                plan.Sessions.Remove(session);
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        public (bool, string) UpdatePlan(Plan plan)
        {
            try
            {
                var existingPlan = _context.plans.FirstOrDefault(p => p.Id == plan.Id);
                if (existingPlan == null)
                {
                    return (false, "Plan not found.");
                }
                existingPlan.Update(plan);
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        //public (bool, string) CancelMemberPlan(int memberPlanId)
        //{
        //    try
        //    {
        //        var memberPlan = _context.plans.FirstOrDefault(mp => mp.Id == memberPlanId);
        //        if (memberPlan == null)
        //        {
        //            return (false, "This member already not found in this plan");
        //        }
        //        memberPlan.GetType().GetProperty("IsActive")?.SetValue(memberPlan, false);
        //        _context.SaveChanges();
        //        return (true, "Member was Canceled from session successfully") ;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public int CountMembersInPlan(int planId)
        //{
        //    try
        //    {
        //        var count = _context.plans.Count(mp => mp.Id == planId);
        //        return count;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}



        //public IEnumerable<MemberPlan> GetPlanMembers(int planId)
        //{
        //    try
        //    {
        //        var members = _context.Set<MemberPlan>().Where(mp => mp.PlanId == planId).ToList();
        //        return members;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


        //public bool MemberAlreadySubscribed(int memberId, int planId)
        //{
        //    try
        //    {
        //        var exists = _context.plans.Any(mp => mp.MemberId == memberId && mp.PlanId == planId && mp.IsActive);
        //        return exists;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


        //public bool SubscribeMemberToPlan(MemberPlan subscription)
        //{
        //    try
        //    {
        //        var result = _context.Set<MemberPlan>().Add(subscription);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


    }
}
