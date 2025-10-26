using AutoMapper;
using AutoMapper.Execution;
using Gym.BLL.ModelVM.Admin;
using Gym.BLL.ModelVM.Attendance;
using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.MemberShip;
using Gym.BLL.ModelVM.Session;
using Gym.BLL.ModelVM.Trainer;
using Gym.BLL.ModelVM.User;
using Gym.DAL.Entities;

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

            CreateMap<Admin, GetAdminVM>()
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<AddAdminVM, Admin>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<EditAdminVM, Admin>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImagePath ?? src.ImagePath))
                .ForMember(dest => dest.UserId, opt => opt.Ignore());


            CreateMap<Attendance, AttendanceMemberVM>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.member.Name))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.member.Image))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.member.Age))
                .ReverseMap();

            CreateMap<CreateAttendanceVM, Attendance>().ReverseMap();
            CreateMap<UpdateAttendanceVM, Attendance>().ReverseMap();

        }
    }
}
