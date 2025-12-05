
using Gym.BLL.ModelVM.MemberPlan;
using Gym.DAL.Entities;
using System.Dynamic;

namespace Gym.BLL.Service.Abstraction
{
    public interface IMemberPlanService
    {
        (bool, string, IEnumerable<GetMemberPlanVM>) GetAll();
        (bool, GetMemberPlanVM?) GetById(int id);
        (bool, string) Create(AddMemberPlanVM memberPlan);
        (bool, string) Update(UpdateMemberPlanVM memberPlan);
        (bool, string) Delete(int id);

        // ===== Member Specific =====
        (bool, string, IEnumerable<GetMemberPlanVM>?) GetMemberPlans(int memberId);
        (bool, string, IEnumerable<GetMemberPlanVM>?) GetActivePlanForMember(int memberId);

        // ===== Validation =====
        bool IsMemberSubscribedToPlan(int memberId, int planId);
        bool HasActivePlan(int memberId);

        // ===== Subscription Management =====
        (bool, string) ActivateSubscription(int memberPlanId);
        (bool, string) CancelSubscription(int memberPlanId);

         (bool, string, List<GetMembersForPlanVM>) GetMembersForPlan(int planid);

    }
}
