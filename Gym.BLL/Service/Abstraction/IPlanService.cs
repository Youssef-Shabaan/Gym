
using Gym.BLL.ModelVM.Plan;
using Gym.BLL.ModelVM.Session;
using Gym.DAL.Entities;

namespace Gym.BLL.Service.Abstraction
{
    public interface IPlanService
    {
        // ===== Plans CRUD =====
        (bool,string, IEnumerable<GetPlanVM>) GetAllPlans();
        (bool,GetPlanVM?) GetPlanById(int id);
        (bool, string) CreatePlan(AddPlanVM plan);
        (bool, string) UpdatePlan(UpdatePlanVM plan);
        (bool, string) DeletePlan(int id);

        // ===== Plan Sessions =====
        (bool, string, IEnumerable<GetSessionVM>) GetPlanSessions(int planId);
        (bool, string) AddSessionToPlan(int planId, int sessionId);
        (bool, string) RemoveSessionFromPlan(int planId, int sessionId);

        
        // ===== Analytics / Helper =====
        bool PlanExists(int planId);
    }
}
