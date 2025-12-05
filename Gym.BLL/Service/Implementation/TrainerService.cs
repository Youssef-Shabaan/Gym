
using AutoMapper;
using Gym.BLL.Helper;
using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.Session;
using Gym.BLL.ModelVM.Trainer;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Gym.DAL.Repo.Implementation;
using Microsoft.AspNetCore.Identity;

namespace Gym.BLL.Service.Implementation
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepo trainerRepo;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        public TrainerService(ITrainerRepo trainerRepo, UserManager<User> userManager, IMapper mapper)
        {
            this.trainerRepo = trainerRepo;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<(bool, string)> Create(AddTrainerVM newtrainer)
        {
            try
            {
                // Create User
                var user = new User()
                {
                    Email = newtrainer.Email,
                    PhoneNumber = newtrainer.PhoneNumber,
                    UserName = newtrainer.UserName,
                    EmailConfirmed = true
                };

                // Save User
                var result = await userManager.CreateAsync(user, newtrainer.Password);

                // Check if User Created
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return (false, errors);
                }

                // Create Trainer
                string imagepath = null;
                if (newtrainer.Image != null)
                    imagepath = Upload.UploadFile("Files", newtrainer.Image);
                var trainer = new Trainer(newtrainer.FisrtName+' '+newtrainer.LastName, imagepath, newtrainer.Gender, newtrainer.Age, newtrainer.Address,  user.Id);
                var addtrainer = trainerRepo.AddTrainer(trainer);
                if (!addtrainer.Item1)
                {
                    await userManager.DeleteAsync(user);
                    return (false, "Faild to Add Trainer");
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
                var trainer = trainerRepo.GetTrainerById(id);
                if (trainer.Item2 == null)
                    return (false, "Not Found");

                string userId = trainer.Item2.userId;

                var delete = trainerRepo.DeleteTrainer(id);
                if (!delete.Item1)
                    return (false, "Failed to Delete Trainer");

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

        public (bool, string, List<GetTrainerVM>) GetAll()
        {
            try
            {
                var result = trainerRepo.GetAllTrainers();
                var mapped = mapper.Map<List<GetTrainerVM>>(result.Item2);
                return (true, "Done", mapped);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, GetTrainerVM) GetByID(int id)
        {
            try
            {
                var result = trainerRepo.GetTrainerById(id);
                if (result.Item2 == null)
                {
                    return (false, "Not Found", null);
                }
                var mapped = mapper.Map<GetTrainerVM>(result.Item2);
                return (true, "Done", mapped);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }
        public (bool, string, GetTrainerVM) GetByUserID(string id)
        {
            try
            {
                var result = trainerRepo.GetTrainerByUserId(id);
                if (result.Item2 == null)
                {
                    return (false, "Not Found", null);
                }
                var mapped = mapper.Map<GetTrainerVM>(result.Item2);
                return (true, "Done", mapped);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, IEnumerable<GetSessionVM>?) GetTrainerSessions(int trainerId)
        {
            try
            {
                var trainer = trainerRepo.GetTrainerSessions(trainerId);
                if (!trainer.Item1)
                {
                    return (false, "Trainer not found", null);
                }
                var sessions = trainer.Item2.Sessions;
                if (!sessions.Any())
                {
                    return (false, "There are no sessions", null);
                }
                var sessionVm = mapper.Map<IEnumerable<GetSessionVM>>(sessions);
                return (true, null, sessionVm);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public int TrainersCount()
        {
            return trainerRepo.TrainersCount(); 
        }

        public (bool, string) Update(int id, UpdateTrainerVM curr)
        {
            try
            {
                var result = trainerRepo.GetTrainerById(id);
                var trainer = result.Item2;
                if (trainer == null)
                    return (false, "Trainer not found");
                
                mapper.Map(curr, trainer);

                var updateResult = trainerRepo.UpdateTrainer(trainer, curr.PhoneNumber);
                if (!updateResult.Item1)
                    return (false, "Failed to update trainer");

                return (true, "Updated successfully");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
