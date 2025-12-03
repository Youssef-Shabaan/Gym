
using AutoMapper;
using Gym.BLL.Helper;
using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.ModifyPhotos;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Gym.DAL.Enums;
using Gym.DAL.Repo.Abstraction;
using Microsoft.AspNetCore.Identity;

namespace Gym.BLL.Service.Implementation
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepo _memberRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<User> userManager;


        public MemberService(IMemberRepo _memberRepo, IMapper _mapper, UserManager<User> userManager)
        {
            this._memberRepo = _memberRepo;
            this._mapper = _mapper;
            this.userManager = userManager;
        }

        

        public async Task<(bool, string)> Create(AddMemberVM newmember, bool ISAdmin)
        {
            try
            {
                // Create User
                var user = new User()
                {
                    Email = newmember.Email,
                    PhoneNumber = newmember.PhoneNumber,
                    UserName = newmember.UserName,
                };

                user.EmailConfirmed = ISAdmin;

                // Save User
                var result = await userManager.CreateAsync(user, newmember.Password);

                // Check if User Created
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return (false, errors);
                }

                // Add role
                await userManager.AddToRoleAsync(user, "Member");

                // Create Member
                string imagepath = null;
                if(newmember.Image!=null)
                   imagepath = Upload.UploadFile("Files", newmember.Image);
                var member = new Member(newmember.FisrtName+" "+newmember.LastName, newmember.Gender, imagepath, newmember.Age, newmember.Address, user.Id);
                var addmember = _memberRepo.Create(member);
                if(!addmember)
                {
                    await userManager.DeleteAsync(user);
                    return (false, "Faild to Add Member");
                }
                return (true, "Added Successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string)> CreateMemberForEmail(string name, Gender gender, string image, int age, string address, string userId)
        {
            try
            {
                var newMember = new Member(name, gender, image, age, address, userId);
                var result = _memberRepo.Create(newMember);
                if(!result)
                {
                    var user = await userManager.FindByIdAsync(userId);
                    await userManager.DeleteAsync(user);
                    return (false, "Faild to Add Member");
                }
                return (true, null);
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
                var member = _memberRepo.GetById(id);
                if (member == null)
                    return (false, "Not Found");

                string userId = member.UserId;

                var delete = _memberRepo.Delete(id);
                if (!delete)
                    return (false, "Failed to Delete Member");

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


        public (bool, string, List<GetMemberVM>) GetAll()
        {
            try
            {
                var result = _memberRepo.GetAll();
                var mapped = _mapper.Map<List<GetMemberVM>>(result);
                return (true, "Done", mapped);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, GetMemberVM) GetByID(int id)
        {
            try
            {
                var result = _memberRepo.GetById(id);
                if (result == null)
                {
                    return (false, "Not Found", null);
                }
                var mapped = _mapper.Map<GetMemberVM>(result);
                return (true, "Done", mapped);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }
        public (bool, string, GetMemberVM) GetByUserID(string id)
        {
            try
            {
                var result = _memberRepo.GetByUserId(id);
                if (result == null)
                {
                    return (false, "Not Found", null);
                }
                var mapped = _mapper.Map<GetMemberVM>(result);
                return (true, "Done", mapped);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string) Update(EditMemberVM curr)
        {
            try
            {
                var member = _memberRepo.GetById(curr.Id);
                if (member == null)
                    return (false, "Member not found");


                var mapMember = _mapper.Map(curr, member);


                var success = _memberRepo.Update(mapMember, curr.PhoneNumber);
                if (!success)
                    return (false, "Failed to update member");

                return (true, "Updated successfully");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public bool ChangePhoto(ChangePhotoVM changePhotoVM)
        {
            try
            {
                var imagePath = Upload.UploadFile("Files", changePhotoVM.ImagePath);
                changePhotoVM.Image = imagePath;
                var memberPhoto = _mapper.Map<Member>(changePhotoVM);
                return _memberRepo.ChangePhoto(memberPhoto);
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public void DeletePhoto(int id)
        {
            _memberRepo.DeletePhoto(id);
        }
    }
}
