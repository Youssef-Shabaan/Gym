
using AutoMapper;
using Gym.BLL.Helper;
using Gym.BLL.ModelVM.Admin;
using Gym.BLL.ModelVM.Member;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Gym.DAL.Repo.Implementation;
using Microsoft.AspNetCore.Identity;

namespace Gym.BLL.Service.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo adminRepo;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        public AdminService(IAdminRepo adminRepo, IMapper mapper, UserManager<User> userManager)
        {
            this.adminRepo = adminRepo;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        public async Task<(bool, string)> Create(AddAdminVM newadmin)
        {
            try
            {
                // Create User
                var user = new User()
                {
                    Email = newadmin.Email,
                    PhoneNumber = newadmin.PhoneNumber,
                    UserName = newadmin.UserName
                };

                // Save User
                var result = await userManager.CreateAsync(user, newadmin.Password);

                // Check if User Created
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return (false, errors);
                }

                // Create Admin
                var imagepath = Upload.UploadFile("Files", newadmin.Image);
                var admin = new Admin(newadmin.Name,imagepath, newadmin.Gender, newadmin.Age, newadmin.Address, user.Id);
                var addmember = adminRepo.Create(admin);
                if (!addmember)
                {
                    await userManager.DeleteAsync(user);
                    return (false, "Faild to Add Admin");
                }
                return (true, "Added Successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string)> Delete(int id)
        {
            try
            {
                var admin = adminRepo.GetById(id);
                if (admin == null)
                    return (false, "Not Found");

                string userId = admin.UserId;

                var delete = adminRepo.Delete(id);
                if (!delete)
                    return (false, "Failed to Delete Admin");

                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                    return (false, "User Not Found");

                var result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                    return (false, "Failed to Delete User");

                return (true, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string, List<GetAdminVM>) GetAll()
        {
            try
            {
                var result = adminRepo.GetAll();
                var mapped = mapper.Map<List<GetAdminVM>>(result);
                return (true, "Done", mapped);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, GetAdminVM) GetByID(int id)
        {
            try
            {
                var result = adminRepo.GetById(id);
                if (result == null)
                {
                    return (false, "Not Found", null);
                }
                var mapped = mapper.Map<GetAdminVM>(result);
                return (true, "Done", mapped);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }
        public (bool, string, GetAdminVM) GetByUserID(string id)
        {
            try
            {
                var result = adminRepo.GetByUserId(id);
                if (result == null)
                {
                    return (false, "Not Found", null);
                }
                var mapped = mapper.Map<GetAdminVM>(result);
                return (true, "Done", mapped);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string) Update(int id, EditAdminVM curr)
        {
            try
            {
                var admin = adminRepo.GetById(id);
                if (admin == null)
                    return (false, "Admin not found");

                if (curr.Image != null)
                {
                    var imagePath = Upload.UploadFile("Files", curr.Image);
                    curr.ImagePath = imagePath;
                }

                mapper.Map(curr, admin);

                var success = adminRepo.Update(admin);
                if (!success)
                    return (false, "Failed to update admin");

                return (true, "Updated successfully");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
