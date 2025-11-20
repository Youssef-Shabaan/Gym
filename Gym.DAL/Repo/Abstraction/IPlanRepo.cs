
using Gym.DAL.Entities;

namespace Gym.DAL.Repo.Abstraction
{
    public interface IPlanRepo
    {
        // ===== Plans CRUD =====
        (bool,string, IEnumerable<Plan>) GetAllPlans();
        (bool,Plan?)  GetPlanById(int id);
        (bool, string) CreatePlan(Plan plan);
        (bool, string) UpdatePlan(Plan plan);
        (bool, string) DeletePlan(int id);

        // ===== Plan Sessions =====
        (bool, string, IEnumerable<Session>) GetPlanSessions(int planId);
        bool AddSessionToPlan(int planId, int sessionId);
        (bool, string) RemoveSessionFromPlan(int planId, int sessionId);

        // ===== Plan Subscriptions (MemberPlan) =====
        //(bool, string, IEnumerable<MemberPlan>) GetPlanMembers(int planId);
        //bool SubscribeMemberToPlan(MemberPlan subscription);
        //(bool, string) CancelMemberPlan(int memberPlanId);

        // ===== Analytics / Helper =====
        //int CountMembersInPlan(int planId);
        bool PlanExists(int planId);
        //bool MemberAlreadySubscribed(int memberId, int planId);
    }
}
