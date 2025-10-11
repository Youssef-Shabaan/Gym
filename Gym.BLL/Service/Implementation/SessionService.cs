
using AutoMapper;
using Gym.BLL.ModelVM.Session;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Microsoft.AspNetCore.Identity;

namespace Gym.BLL.Service.Implementation
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepo sessionRepo;
        private readonly IMapper mapper;
        public SessionService(ISessionRepo sessionRepo, IMapper mapper)
        {
            this.mapper = mapper;
            this.sessionRepo = sessionRepo;
        }
        public (bool, string?) AddSession(AddUpdateSessionVM sessionvm)
        {
            try
            {
                var session = mapper.Map<Session>(sessionvm);
                var result = sessionRepo.AddSession(session);
                if(!result.Item1)
                {
                    return (false, result.Item2);
                }
                return (true, "Added Successfully");
            }
            catch (Exception ex)
            {
                return(false, ex.Message);
            }
        }

        public (bool, string?) Delete(int id)
        {
            try
            {
                var result = sessionRepo.Delete(id);
                if(!result.Item1)
                {
                    return(false, result.Item2);
                }
                return(true, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string, IEnumerable<GetSessionVM>?) GetAll()
        {
            try
            {
                var AllSessions = sessionRepo.GetAll();
                if(!AllSessions.Item1)
                {
                    return (false, "There are no sessions", null);
                }
                var Sessions = mapper.Map<IEnumerable<GetSessionVM>>(AllSessions.Item2);
                return(true, null,  Sessions);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, GetSessionVM?) GetById(int id)
        {
            try
            {
                var session = sessionRepo.GetById(id);
                if(!session.Item1)
                {
                    return (false, null);
                }
                var mapSession = mapper.Map<GetSessionVM>(session.Item2);
                return (true, mapSession);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public (bool, string, IEnumerable<GetSessionVM>?) GetPastSessions()
        {
            try
            {
                var AllSessions = sessionRepo.GetPastSessions();
                if (!AllSessions.Item1)
                {
                    return (false, "There are no sessions", null);
                }
                var Sessions = mapper.Map<IEnumerable<GetSessionVM>>(AllSessions.Item2);
                return (true, null, Sessions);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, IEnumerable<GetSessionVM>?) GetSessionsByTrainerId(int trainerId)
        {
            try
            {
                var TrainerSessions = sessionRepo.GetSessionsByTrainerId(trainerId);
                if(!TrainerSessions.Item1)
                {
                    return (false, "There are no sessions", null);
                }
                var sessions = mapper.Map<IEnumerable<GetSessionVM>>(TrainerSessions.Item2);
                return (true, null, sessions);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, IEnumerable<GetSessionVM>?) GetUpcomingSessions()
        {
            try
            {
                var AllSessions = sessionRepo.GetUpcomingSessions();
                if (!AllSessions.Item1)
                {
                    return (false, "There are no sessions", null);
                }
                var Sessions = mapper.Map<IEnumerable<GetSessionVM>>(AllSessions.Item2);
                return (true, null, Sessions);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string?) Update(AddUpdateSessionVM sessionvm)
        {
            try
            {
                var session = mapper.Map<Session>(sessionvm);
                var result = sessionRepo.Update(session);
                if(!result.Item1)
                {
                    return (false, result.Item2);
                }
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
