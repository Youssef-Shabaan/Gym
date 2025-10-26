
using AutoMapper;
using Gym.BLL.ModelVM.User;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Repo.Abstraction;
using Gym.DAL.Repo.Implementation;

namespace Gym.BLL.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepo userRepo;
        private readonly IMapper mapper;
        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
        }

        public (bool, string, List<GetUserVM>) GetAllUsers()
        {
            try
            {
                var users = userRepo.GetAll();
                if (users == null || users.Count == 0)
                    return (false, "No users found", null);
                var mappedUsers = mapper.Map<List<GetUserVM>>(users);
                return (true, "Users retrieved successfully", mappedUsers);

            }
            catch (Exception ex) { return (false, ex.Message, null); }
        }

        public (bool, string, GetUserVM) GetUserById(string id)
        {
            try { 
                var user = userRepo.GetById(id);
                if (user == null) return (false, "User not founded", null);
                var mappeduser = mapper.Map<GetUserVM>(user);
                return (true, "User retrieved successfully", mappeduser);
            }
            catch (Exception ex) { return (false, ex.Message, null); }
        }
    }
}
