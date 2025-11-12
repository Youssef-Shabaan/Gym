using AutoMapper;
using AutoMapper.Execution;
using Gym.BLL.ModelVM.Admin;
using Gym.BLL.ModelVM.Attendance;
using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.MemberSession;
using Gym.BLL.ModelVM.MemberShip;
using Gym.BLL.ModelVM.ModifyPhotos;
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
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Sessions, opt => opt.MapFrom(src => src.Sessions))
                .ReverseMap();

            CreateMap<UpdateTrainerVM, Trainer>().ReverseMap();

            CreateMap<ChangePhotoVM, DAL.Entities.Member>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.Id));


            CreateMap<Gym.DAL.Entities.Member, GetMemberVM>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ReverseMap();


            CreateMap<AddMemberVM, Gym.DAL.Entities.Member>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<EditMemberVM, Gym.DAL.Entities.Member>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForPath(dest => dest.User.PhoneNumber, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AddSessionVM, Session>().ReverseMap();

            CreateMap<UpdateSessionVM, Session>().ReverseMap();

            CreateMap<Session, GetSessionVM>()
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src._Trainer.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                DateTime.Now < src.StartTime ? "Not Started" : DateTime.Now >= src.StartTime && DateTime.Now <= src.EndTime ? "Ongoing" : "Ended"))
                .ReverseMap();

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


            CreateMap<AddMemberSessionVM, MemberSession>().ReverseMap();

            CreateMap<MemberSession, GetMemberSessionVM>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.TrainerSubscription.Trainer.Name))
                .ForMember(dest => dest.SessionName, opt => opt.MapFrom(src => src.Session.Name))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.Payment.Status))
                .ReverseMap();

            CreateMap<UpdateMemberSessionVM, MemberSession>().ReverseMap();

        }
    }
}
