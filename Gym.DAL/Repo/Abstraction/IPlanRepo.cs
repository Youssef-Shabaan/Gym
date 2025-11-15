
using Gym.DAL.Entities;

namespace Gym.DAL.Repo.Abstraction
{
    public interface IPlanRepo
    {
        // ===== Plans CRUD =====
        IEnumerable<Plan> GetAllPlans();
        Plan? GetPlanById(int id);
        bool CreatePlan(Plan plan);
        bool UpdatePlan(Plan plan);
        bool DeletePlan(int id);

        // ===== Plan Sessions =====
        IEnumerable<Session> GetPlanSessions(int planId);
        bool AddSessionToPlan(int planId, int sessionId);
        bool RemoveSessionFromPlan(int planId, int sessionId);

        // ===== Plan Subscriptions (MemberPlan) =====
        IEnumerable<MemberPlan> GetPlanMembers(int planId);
        bool SubscribeMemberToPlan(MemberPlan subscription);
        bool CancelMemberPlan(int memberPlanId);

        // ===== Analytics / Helper =====
        int CountMembersInPlan(int planId);
        bool PlanExists(int planId);
        bool MemberAlreadySubscribed(int memberId, int planId);
    }
}
