

namespace Gym.BLL.ModelVM.Session
{
    public class GetSessionVM
    {
        public string Name { get;  set; }
        public string? Description { get;  set; }
        public DateTime ScheduleTime { get;  set; }
        public int Count { get; set; }
        public int Capacity { get;  set; }
    }
}
