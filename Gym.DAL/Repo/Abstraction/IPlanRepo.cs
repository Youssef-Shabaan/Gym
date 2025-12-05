
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

        bool PlanExists(int planId);
        int CountPlanForTrainer(int trainerid);
        (bool, IEnumerable<Plan>?) GetPlansByTrainerId(int trainerId);

        int PlansCount();
    }
}
