
using Gym.BLL.ModelVM.Session;

namespace Gym.BLL.ModelVM.Trainer
{
    public class GetTrainerVM
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Info { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<GetSessionVM> Sessions { get; set; }
    }
}
