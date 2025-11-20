
using AutoMapper;
using Gym.BLL.ModelVM.Plan;
using Gym.BLL.ModelVM.Session;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;

namespace Gym.BLL.Service.Implementation
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepo planRepo;
        private readonly IMapper mapper;

        public PlanService(IPlanRepo planRepo, IMapper mapper)
        {
            this.planRepo = planRepo;
            this.mapper = mapper;
        }

        public (bool, string) AddSessionToPlan(int planId, int sessionId)
        {
            try
            {
                var result = planRepo.AddSessionToPlan(planId, sessionId);
                if(!result)
                {
                    return (false, "Failed to add session to the plan");
                }
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string) CreatePlan(AddPlanVM planVM)
        {
            try
            {
                var plan = mapper.Map<Plan>(planVM);
                var result = planRepo.CreatePlan(plan);
                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message); 
            }
        }

        public (bool, string) DeletePlan(int id)
        {
            try
            {
                var result = planRepo.DeletePlan(id);
                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string, IEnumerable<GetPlanVM>) GetAllPlans()
        {
            try
            {
                var allPlans = planRepo.GetAllPlans();  
                if(!allPlans.Item1)
                {
                    return(false,allPlans.Item2, null);
                }
                var allPlanVM = mapper.Map<IEnumerable<GetPlanVM>>(allPlans.Item3);   
                return(true, null, allPlanVM);  
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);   
            }
        }

        public (bool, GetPlanVM?) GetPlanById(int id)
        {
            try
            {
                var plan = planRepo.GetPlanById(id);    
                if(!plan.Item1)
                {
                    return (false, null);   
                }
                var planVM = mapper.Map<GetPlanVM>(plan.Item2);
                return(true, planVM);
            }
            catch(Exception ex)
            {
                return (false, null);
            }
        }

        public (bool, string, IEnumerable<GetSessionVM>) GetPlanSessions(int planId)
        {
            try
            {
                var sessions = planRepo.GetPlanSessions(planId);
                if(!sessions.Item1)
                {
                    return (true, sessions.Item2, null);
                }
                var sessionsVM = mapper.Map<IEnumerable<GetSessionVM>>(sessions.Item3);
                return(true, null,  sessionsVM);
            }
            catch(Exception ex)
            {
                return(false,  ex.Message, null);   
            }
        }

        public bool PlanExists(int planId)
        {
            try
            {
                return planRepo.PlanExists(planId); 
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
                var result = planRepo.RemoveSessionFromPlan(planId, sessionId);
                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string) UpdatePlan(UpdatePlanVM planVM)
        {
            try
            {
                var plan = mapper.Map<Plan>(planVM);
                var result = planRepo.UpdatePlan(plan); 
                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message); 
            }
        }
    }
}
