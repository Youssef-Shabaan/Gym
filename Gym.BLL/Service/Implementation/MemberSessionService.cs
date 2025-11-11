using AutoMapper;
using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.Session;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Repo.Abstraction;

namespace Gym.BLL.Service.Implementation
{
    public class MemberSessionService : IMemberSessionService
    {
        private readonly IMemberSessionRepo _memberSessionRepo;
        private readonly IMapper _mapper;

        public MemberSessionService(IMemberSessionRepo memberSessionRepo, IMapper mapper)
        {
            _memberSessionRepo = memberSessionRepo;
            _mapper = mapper;
        }

        
    }
}
