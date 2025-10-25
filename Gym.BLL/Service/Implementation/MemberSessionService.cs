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

        public (bool, string) AddMemberToSession(int memberId, int sessionId)
        {
            return _memberSessionRepo.AddMemberToSession(memberId, sessionId);
        }

        public (bool, string) RemoveMemberFromSession(int memberId, int sessionId)
        {
            return _memberSessionRepo.RemoveMemberFromSession(memberId, sessionId);
        }

        public (bool, string, IEnumerable<GetSessionVM>?) GetSessionsByMember(int memberId)
        {
            var result = _memberSessionRepo.GetSessionsByMember(memberId);
            if (!result.Item1 || result.Item3 == null)
                return (false, result.Item2, null);

            var mapped = _mapper.Map<IEnumerable<GetSessionVM>>(result.Item3);
            return (true, "Done", mapped);
        }

        public (bool, string, IEnumerable<GetMemberVM>?) GetMembersBySession(int sessionId)
        {
            var result = _memberSessionRepo.GetMembersBySession(sessionId);
            if (!result.Item1 || result.Item3 == null)
                return (false, result.Item2, null);

            var mapped = _mapper.Map<IEnumerable<GetMemberVM>>(result.Item3);
            return (true, "Done", mapped);
        }

        public (bool, string) RemoveAllSessionsForMember(int memberId)
        {
            return _memberSessionRepo.RemoveAllSessionsForMember(memberId);
        }

        public (bool, string) RemoveAllMembersFromSession(int sessionId)
        {
            return _memberSessionRepo.RemoveAllMembersFromSession(sessionId);
        }
    }
}
