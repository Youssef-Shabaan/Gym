using AutoMapper;
using Gym.BLL.ModelVM.Session;
using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.Trainer;
using Gym.DAL.Entities;
using Gym.BLL.ModelVM.MemberShip;
using Gym.BLL.ModelVM.User;
using AutoMapper.Execution;

namespace Gym.BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Trainer, GetTrainerVM>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Sessions, opt => opt.MapFrom(src => src.Sessions))
                .ReverseMap();

            CreateMap<UpdateTrainerVM, Trainer>().ReverseMap();


            CreateMap<Gym.DAL.Entities.Member, GetMemberVM>()
    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));


            CreateMap<AddMemberVM, Gym.DAL.Entities.Member>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<EditMemberVM, Gym.DAL.Entities.Member>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AddUpdateSessionVM, Session>().ReverseMap();
            CreateMap<Session, GetSessionVM>().ReverseMap();

            CreateMap<GetAllMemberShipVM, MemberShip>().ReverseMap();
            CreateMap<GetMemberShipVM, MemberShip>().ReverseMap();

            CreateMap<GetUserVM, User>().ReverseMap();
        }
    }
}
