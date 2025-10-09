
namespace Gym.BLL.ModelVM.Trainer
{
    public class GetTrainerSessionVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ScheduleTime { get; set; }  
        public string State { get; set; }
        public int Count { get; set; }
        public int Capacity { get; set; }
    }
}
