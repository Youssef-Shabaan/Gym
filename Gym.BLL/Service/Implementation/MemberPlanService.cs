
using AutoMapper;
using AutoMapper.Execution;
using Gym.BLL.ModelVM.MemberPlan;
using Gym.BLL.ModelVM.MemberSession;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Gym.DAL.Repo.Implementation;

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

        public (bool, string) Add(AddMemberPlanVM vm)
        {
            try
            {
                var entity = new MemberPlan
                {
                    MemberId = vm.MemberId,
                    PlanId = vm.PlanId,
                    BookingDate = vm.BookingDate,
                    Status = vm.Status,
                    Price = vm.Price
                };

                var result = _memberPlanRepo.Create(entity);
                return result;

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public (bool, string) AddMemberToPlan(string userId, int planId)
        {
            try
            {
                var member = _memberPlanRepo.GetMemberByUserId(userId);
                if (member == null)
                    return (false, "Member not found");
                var plan = _memberPlanRepo.GetPlanById(planId);
                if (plan == null)
                    return (false, "Session not found");
                if (plan.Booked >= plan.Capcity)
                    return (false, "Session is full");
                var alreadyBooked = _memberPlanRepo.IsMemberBooked(member.MemberId, planId);
                if (alreadyBooked)
                    return (false, "Already booked");
                var addVM = new AddMemberPlanVM
                {
                    MemberId = member.MemberId,
                    PlanId = planId,
                    BookingDate = DateTime.Now,
                    Status = "Booked",
                    Price = plan.Price
                };
                var result = Add(addVM);
                if (!result.Item1)
                    return result;
                return (true, "Booked successfully");

            }
            catch (Exception ex)
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

        public (bool, string, List<GetMembersForPlanVM>) GetMembersForPlan(int planid)
        {
            try
            {
                var members = _memberPlanRepo.GetMembersForPlan(planid);
                if (!members.Item1)
                {
                    return (false, members.Item2, null);
                }
                var mapMembers = _mapper.Map<List<GetMembersForPlanVM>>(members.Item3);
                return (true, null, mapMembers);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
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
