
using Gym.DAL.Enums;
using Microsoft.AspNetCore.Identity;

namespace Gym.DAL.Entities
{
    public class User : IdentityUser
    {
        public UserType UserType { get; private set; }
        public User() { }
        public User(UserType userType)
        {
            this.UserType = userType;
        }
        public bool EditUser(User user)
        {
            if (user == null) return false;
            this.UserType = user.UserType;
            return true;
        }
    }
}
