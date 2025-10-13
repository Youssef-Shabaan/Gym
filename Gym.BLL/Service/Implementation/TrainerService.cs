
using AutoMapper;
using Gym.BLL.Helper;
using Gym.BLL.ModelVM.Trainer;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Microsoft.AspNetCore.Identity;

namespace Gym.BLL.Service.Implementation
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepo trainerRepo;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        public TrainerService(ITrainerRepo trainerRepo, UserManager<User> userManager,IMapper mapper)
        {
            this.trainerRepo = trainerRepo;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<(bool, string?)> AddTrainer(AddTrainerVM trainervm)
        {
            try
            {
                User user = new User() { Email = trainervm.Email, PhoneNumber = trainervm.PhoneNumber, UserName = trainervm.Email};

                var result = await userManager.CreateAsync(user, trainervm.Password);

                if (!result.Succeeded)
                {
                    return (false, "Error in creation new trainer");
                }

                var imagePath = Upload.UploadFile("Files", trainervm.Image);

                Trainer newTrainer = new Trainer(trainervm.Name, imagePath, trainervm.Age, trainervm.Info, trainervm.Address, trainervm.Capacity);
                var trainer = trainerRepo.AddTrainer(newTrainer);
                if(!trainer.Item1)
                {
                    return(false, trainer.Item2);
                }
                return (true, null);
                
            }
            catch (Exception ex)
            {
                return(false, null);
            }
        }

        public (bool, string?) DeleteTrainer(int id)
        {
            try
            {
                var result = trainerRepo.DeleteTrainer(id);
                if(!result.Item1)
                {
                    return (false, result.Item2);
                }
                return (true, null) ;
            }
            catch(Exception ex)
            {
                return(false, ex.Message);
            }
        }

        public (bool, IEnumerable<GetTrainerVM>?) GetAllTrainers()
        {
            try
            {
                var trainers = trainerRepo.GetAllTrainers();
                if(!trainers.Item1)
                {
                    return(false, null);
                }
                var trainersVM = mapper.Map<IEnumerable<GetTrainerVM>>(trainers);
                return (true, trainersVM);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public (bool, GetTrainerVM?) GetTrainerById(int id)
        {
            try
            {
                var trainers = trainerRepo.GetTrainerById(id);
                if (!trainers.Item1)
                {
                    return (false, null);
                }
                var trainersVM = mapper.Map<GetTrainerVM>(trainers);
                return (true, trainersVM);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public int GetTrainerCount()
        {
            return trainerRepo.GetTrainerCount();
        }

        public (bool, IEnumerable<GetTrainerSessionVM>?) GetTrainerSessions(int trainerId)
        {
            try
            {
                var trainer = trainerRepo.GetTrainerSessions(trainerId);
                if(!trainer.Item1)
                {
                    return(false, null);
                }
                var trainerSessions = new List<GetTrainerSessionVM>();
                foreach (var session in trainer.Item2.Sessions)
                {
                    
                    trainerSessions.Add(new GetTrainerSessionVM
                    {
                        Name = session.Name,
                        Description = session.Description,
                        StartTime= session.StartTime,
                        EndTime = session.EndTime,
                        State = session.EndTime < DateTime.Now ? "Past" :(DateTime.Now>session.StartTime?"Playing":"Upcomming")
                    });
                }
                return(true, trainerSessions);
            }
            catch(Exception ex)
            {
                return (false, null);
            }
        }

        public (bool, string?) RestoreTrainer(int id)
        {
            try
            {
                var result = trainerRepo.RestoreTrainer(id);
                if (!result.Item1)
                {
                    return(false, result.Item2);
                }
                return (true, null);
            }
            catch (Exception ex)
            {
                return(true, ex.Message);
            }
        }

        public (bool, string?) UpdateTrainer(UpdateTrainerVM trainervm)
        {

            try
            {
                var trainer = mapper.Map<Trainer>(trainervm);
                var result = trainerRepo.UpdateTrainer(trainer);
                if(!result.Item1)
                {
                    return(false, result.Item2);
                }
                return(true, null);
            }
            catch (Exception ex) 
            {
                return(false, ex.Message);
            }
        }
    }
}
