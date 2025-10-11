using AutoMapper;
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
                .ForMember(a => a.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ReverseMap();

            CreateMap<UpdateTrainerVM, Trainer>()
                .ForPath(a => a.User.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
            CreateMap<Member, GetMemberVM>()
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User != null ? src.User.Email : null))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User != null ? src.User.PhoneNumber : null))
                .ReverseMap();

            CreateMap<AddMemberVM, Member>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<EditMemberVM, Member>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ReverseMap();

                .ForPath(a => a.User.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();
        }
    }
}
