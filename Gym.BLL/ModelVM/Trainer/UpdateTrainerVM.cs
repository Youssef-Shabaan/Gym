using Gym.DAL.Enums;
using Microsoft.AspNetCore.Http;

namespace Gym.BLL.ModelVM.Trainer
{
    public class UpdateTrainerVM
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Info { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public Gender? Gender { get; set; }
        public int Capacity { get; set; }
    }
}
