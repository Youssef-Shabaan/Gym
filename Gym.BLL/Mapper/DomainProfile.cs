using AutoMapper;
using Gym.BLL.ModelVM.Session;
using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.Trainer;
using Gym.DAL.Entities;

namespace Gym.BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Trainer, GetTrainerVM>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();

            CreateMap<UpdateTrainerVM, Trainer>().ReverseMap();

            CreateMap<Member, GetMemberVM>()
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();

            CreateMap<AddMemberVM, Member>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<EditMemberVM, Member>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AddUpdateSessionVM, Session>().ReverseMap();
            CreateMap<Session, GetSessionVM>().ReverseMap();
        }
    }
}
