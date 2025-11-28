
using AutoMapper;
using AutoMapper.Execution;
using Gym.BLL.ModelVM.MemberPlan;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;

namespace Gym.BLL.Service.Implementation
{
    public class MemberPlanService : IMemberPlanService
    {
        private readonly IMemberPlanRepo _memberPlanRepo;
        private readonly IMemberRepo _memberRepo;
        private readonly IPlanRepo _PlanRepo;
        private readonly IMapper _mapper;
        public MemberPlanService(IMemberPlanRepo memberPlanRepo, IMemberRepo memberRepo, IPlanRepo planRepo, IMapper mapper)
        {
            _memberPlanRepo = memberPlanRepo;
            _memberRepo = memberRepo;
            _PlanRepo = planRepo;   
            _mapper = mapper;
        }

        public (bool, string) ActivateSubscription(int memberPlanId)
        {
            try
            {
                var result = _memberPlanRepo.ActivateSubscription(memberPlanId);
                return result;
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
                var result = _memberPlanRepo.CancelSubscription(memberPlanId);
                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string) Create(AddMemberPlanVM memberPlan)
        {
            try
            {
                var plan = _PlanRepo.PlanExists(memberPlan.PlanId);
                if (!plan)
                {
                    return (false, "Plan not found");
                }
                var member = _memberRepo.MemberExist(memberPlan.MemberId);
                if(!member)
                {
                    return (false, "Member not found");
                }

                var memberplan = _mapper.Map<MemberPlan>(memberPlan);
                
                var result = _memberPlanRepo.Create(memberplan);
                return result;
            }
            catch(Exception ex) 
            {
                return (false, ex.Message);
            }
        }

        public (bool, string) Delete(int id)
        {
            return _memberPlanRepo.Delete(id);
        }

        public (bool, string, IEnumerable<GetMemberPlanVM>?) GetActivePlanForMember(int memberId)
        {
            try
            {
                var memberPlans = _memberPlanRepo.GetActivePlanForMember(memberId);
                if (!memberPlans.Item1)
                {
                    return (false, memberPlans.Item2, null);
                }
                var mapMemberPlans = _mapper.Map<IEnumerable<GetMemberPlanVM>>(memberPlans.Item3);
                return (true, null, mapMemberPlans);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, IEnumerable<GetMemberPlanVM>) GetAll()
        {
            try
            {
                var memberPlans = _memberPlanRepo.GetAll();
                if (!memberPlans.Item1)
                {
                    return (false, memberPlans.Item2, null);
                }
                var mapMemberPlans = _mapper.Map<IEnumerable<GetMemberPlanVM>>(memberPlans.Item3);
                return (true, null, mapMemberPlans);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, GetMemberPlanVM?) GetById(int id)
        {
            try
            {
                var memberPlan = _memberPlanRepo.GetById(id);
                if (!memberPlan.Item1)
                {
                    return (false, null);
                }
                var mapMemberPlan = _mapper.Map<GetMemberPlanVM>(memberPlan.Item2);
                return (true, mapMemberPlan);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public (bool, string, IEnumerable<GetMemberPlanVM>?) GetMemberPlans(int memberId)
        {
            try
            {
                var memberPlans = _memberPlanRepo.GetMemberPlans(memberId);
                if(!memberPlans.Item1)
                {
                    return (false, memberPlans.Item2, null);
                }
                var mapMemberPlans = _mapper.Map<IEnumerable<GetMemberPlanVM>>(memberPlans.Item3);
                return(true, null, mapMemberPlans); 
            }
            catch (Exception ex)
            {
                return(false, ex.Message, null);
            }
        }

        public bool HasActivePlan(int memberId)
        {
            return _memberPlanRepo.HasActivePlan(memberId);
        }

        public bool IsMemberSubscribedToPlan(int memberId, int planId)
        {
            return _memberPlanRepo.IsMemberSubscribedToPlan(memberId, planId);
        }

        public (bool, string) Update(UpdateMemberPlanVM memberPlan)
        {
            try
            {
                var memberplan = _mapper.Map<MemberPlan>(memberPlan);
                var result = _memberPlanRepo.Update(memberplan);
                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message); 
            }
        }
    }
}
