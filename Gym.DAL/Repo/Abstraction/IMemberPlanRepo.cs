
using Gym.DAL.Entities;

namespace Gym.DAL.Repo.Abstraction
{
    public interface IMemberPlanRepo
    {
        // ===== CRUD =====
        IEnumerable<MemberPlan> GetAll();
        MemberPlan? GetById(int id);
        bool Create(MemberPlan memberPlan);
        bool Update(MemberPlan memberPlan);
        bool Delete(int id);

        // ===== Member Specific =====
        IEnumerable<MemberPlan> GetMemberPlans(int memberId);
        MemberPlan? GetActivePlanForMember(int memberId);

        // ===== Plan Specific =====
        IEnumerable<MemberPlan> GetPlanMembers(int planId);

        // ===== Validation =====
        bool IsMemberSubscribedToPlan(int memberId, int planId);
        bool HasActivePlan(int memberId);

        // ===== Subscription Management =====
        bool ActivateSubscription(int memberPlanId);
        bool CancelSubscription(int memberPlanId);
    }
}
