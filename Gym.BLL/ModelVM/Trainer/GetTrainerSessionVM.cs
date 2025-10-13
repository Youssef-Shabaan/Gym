
namespace Gym.BLL.ModelVM.Trainer
{
    public class GetTrainerSessionVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }  
        public DateTime EndTime { get; set; }  
        public string State { get; set; }
    }
}
