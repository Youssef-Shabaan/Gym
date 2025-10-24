using AutoMapper;
using Gym.BLL.ModelVM.Session;
using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.Trainer;
using Gym.DAL.Entities;
using Gym.BLL.ModelVM.MemberShip;

namespace Gym.BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Trainer, GetTrainerVM>()
                .ReverseMap();

            CreateMap<UpdateTrainerVM, Trainer>().ReverseMap();

            CreateMap<Member, GetMemberVM>()
                .ReverseMap();

            CreateMap<AddMemberVM, Member>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<EditMemberVM, Member>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AddUpdateSessionVM, Session>().ReverseMap();
            CreateMap<Session, GetSessionVM>().ReverseMap();

            CreateMap<GetAllMemberShipVM, MemberShip>().ReverseMap();
            CreateMap<GetMemberShipVM, MemberShip>().ReverseMap();
        }
    }
}
