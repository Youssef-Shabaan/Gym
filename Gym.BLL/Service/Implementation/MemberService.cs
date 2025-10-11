
using AutoMapper;
using Gym.BLL.ModelVM.Member;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;

namespace Gym.BLL.Service.Implementation
{
    public class MemberService : IMemberService
    {
        public readonly IMemberRepo _memberRepo;
        public readonly IMapper _mapper;
        public MemberService(IMemberRepo _memberRepo, IMapper _mapper)
        {
            this._memberRepo = _memberRepo;
            this._mapper = _mapper;
        }
        public (bool, string) Create(AddMemberVM newmember)
        {
            try
            {
                var mapped = _mapper.Map<Member>(newmember);
                var result = _memberRepo.Create(mapped);
                if (!result)
                {
                    return (false, "Faild to Add");
                }
                return (true, "Added Successfully");
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
                var result = _memberRepo.Delete(id);
                if (!result)
                {
                    return (false, "Faild to Delete");
                }
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

        public (bool, string) Update(int id, EditMemberVM curr)
        {
            try
            {
                var member = _memberRepo.GetById(id);
                if (member == null)
                {
                    return (false, "Not Found");
                }
                _mapper.Map(curr, member);
                var save = _memberRepo.Update(member);
                if (!save)
                {
                    return (false, "Failed to Update");
                }

                return (true, "Updated Successfully");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
