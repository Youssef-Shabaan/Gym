
using Gym.DAL.Entities;

namespace Gym.DAL.Repo.Abstraction
{
    public interface IMemberPlanRepo
    {
        // ===== CRUD =====
        (bool, string, IEnumerable<MemberPlan>) GetAll();
        (bool, MemberPlan?) GetById(int id);
        (bool, string) Create(MemberPlan memberPlan);
        (bool, string) Update(MemberPlan memberPlan);
        (bool, string) Delete(int id);

        // ===== Member Specific =====
        (bool, string ,IEnumerable<MemberPlan>?) GetMemberPlans(int memberId);
        (bool, string , IEnumerable<MemberPlan>?) GetActivePlanForMember(int memberId);

        // ===== Validation =====
        bool IsMemberSubscribedToPlan(int memberId, int planId);
        bool HasActivePlan(int memberId);

        // ===== Subscription Management =====
        (bool, string) ActivateSubscription(int memberPlanId);
        (bool, string) CancelSubscription(int memberPlanId);

        public (bool, string, List<MemberPlan>) GetMembersForPlan(int planid);

    }
}
