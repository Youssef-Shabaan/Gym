
using Gym.BLL.ModelVM.Session;
using Gym.DAL.Enums;

namespace Gym.BLL.ModelVM.Trainer
{
    public class GetTrainerVM
    {
        public string UserId { get; set; }
        public int TrainerId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? JoinDate { get; set; }
        public string? UserName { get; set; }
        public ICollection<GetSessionVM> Sessions { get; set; }
    }
}
