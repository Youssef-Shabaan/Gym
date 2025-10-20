
using AutoMapper;
using Gym.BLL.ModelVM.MemberShip;
using Gym.BLL.Service.Abstraction;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;

namespace Gym.BLL.Service.Implementation
{
    public class MemberShipService : IMemberShipService
    {
        private readonly IMemberShipRepo _memberShipRepo;
        private readonly IMapper _mapper;
        public MemberShipService(IMemberShipRepo memberShipRepo, IMapper mapper)
        {
            _memberShipRepo = memberShipRepo;
            _mapper = mapper;
        }
        public bool Create(MemberShip newMemberShip)
        {
            try
            {
                var result = _memberShipRepo.Create(newMemberShip);
                return result;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var result = _memberShipRepo.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public (bool, string, List<GetAllMemberShipVM>) GetAll()
        {
            try
            {
                var result = _memberShipRepo.GetAll();
                if(!result.Item1)
                {
                    return (false, result.Item2, null);
                }
                var MapMemberShip = _mapper.Map<List<GetAllMemberShipVM>>(result.Item3);
                return(true, null, MapMemberShip);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public (bool,string, GetMemberShipVM) GetById(int id)
        {
            try
            {
                var membership = _memberShipRepo.GetById(id);
                if(!membership.Item1)
                {
                    return (false, "There is no member ship", null);
                }
                var mapMebership = _mapper.Map<GetMemberShipVM>(membership.Item2);
                return (true, null, mapMebership);
            }
            catch (Exception ex)
            {
                return (false,ex.Message, null);
            }
        }

        public bool Update(MemberShip newMemberShip)
        {
            try
            {
                var result = _memberShipRepo.Update(newMemberShip);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
