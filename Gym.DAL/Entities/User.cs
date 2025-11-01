
using Gym.DAL.Enums;
using Microsoft.AspNetCore.Identity;

namespace Gym.DAL.Entities
{
    public class User : IdentityUser
    {
        public User() { }
        public User(string phone, string email)
        {
            this.PhoneNumber = phone;
            this.Email = email;
            this.UserName = email;
        }
        public bool EditUser(string phone)
        {
            this.PhoneNumber = phone;
            return true;
        }
    }
}
