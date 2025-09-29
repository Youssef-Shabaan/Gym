using Gym.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }   
        public string? Address { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.Now;

    }
}
