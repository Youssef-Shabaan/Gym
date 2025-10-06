
using Gym.DAL.Enums;
using Microsoft.AspNetCore.Identity;

namespace Gym.DAL.Entities
{
    public class User : IdentityUser
    {
        public UserType UserType { get; private set; }
    }
}
