using AutoMapper;
using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.MemberSession;
using Gym.BLL.ModelVM.Session;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
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

        public (bool, string) Add(AddMemberSessionVM memberSessionVM)
        {
            try
            {
                var memberSession = _mapper.Map<MemberSession>(memberSessionVM);
                var result = _memberSessionRepo.Add(memberSession);
                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string) Delete(int id)
        {
            try
            {
                var result = _memberSessionRepo.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string, IEnumerable<GetMemberSessionVM>?) GetAll()
        {
            try
            {
                var AllMemberSessions = _memberSessionRepo.GetAll();
                if(!AllMemberSessions.Item1)
                {
                    return (false, AllMemberSessions.Item2, null);
                }
                var result = _mapper.Map<IEnumerable<GetMemberSessionVM>>(AllMemberSessions);
                return (true, null, result);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, GetMemberSessionVM?) GetById(int Id)
        {
            try
            {
                var memberSession = _memberSessionRepo.GetById(Id);
                if(!memberSession.Item1)
                {
                    return(false, memberSession.Item2, null);   
                }
                var result = _mapper.Map<GetMemberSessionVM>(memberSession);
                return (true, null, result);
            }
            catch (Exception ex)
            {
                return(false, ex.Message, null);
            }
        }

        public (bool, string, IEnumerable<GetMemberSessionVM>?) GetByMemberId(int MemberId)
        {
            try
            {
                var AllSessionsForMember = _memberSessionRepo.GetByMemberId(MemberId);
                if (!AllSessionsForMember.Item1)
                {
                    return(false,  AllSessionsForMember.Item2, null);   
                }
                var result = _mapper.Map<IEnumerable<GetMemberSessionVM>>(AllSessionsForMember);
                return (true, null, result);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool, string, IEnumerable<GetMemberSessionVM>?) GetBySessionId(int SessionId)
        {
            try
            {
                var AllMemberForSession = _memberSessionRepo.GetBySessionId(SessionId);
                if (!AllMemberForSession.Item1)
                {
                    return(false, AllMemberForSession.Item2, null); 
                }
                var result = _mapper.Map<IEnumerable<GetMemberSessionVM>>(AllMemberForSession);
                return (true, null, result);
            }
            catch (Exception ex)
            {
                return(true, ex.Message, null); 
            }
        }

        public (bool, string, IEnumerable<GetMemberSessionVM>?) GetByTrainerSubscriptionId(int trainerSubscriptionId)
        {
            try
            {
                var memberSession = _memberSessionRepo.GetByTrainerSubscriptionId(trainerSubscriptionId);
                if (!memberSession.Item1)
                {
                    return (false, memberSession.Item2, null);
                }
                var result = _mapper.Map<IEnumerable<GetMemberSessionVM>>(memberSession);
                return (true, null, result);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string) SetAttendance(int memberId, int sessionId)
        {
            try
            {
                var result = _memberSessionRepo.SetAttendance(memberId, sessionId);
                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string) Update(UpdateMemberSessionVM memberSession)
        {
            try
            {
                var membersession = _mapper.Map<MemberSession>(memberSession);
                var result = _memberSessionRepo.Update(membersession);  
                return result;
            }
            catch (Exception ex)
            {
                return(false, ex.Message);
            }
        }
    }
}
