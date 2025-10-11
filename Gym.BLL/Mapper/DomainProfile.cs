using AutoMapper;
using Gym.BLL.ModelVM.Session;
using Gym.BLL.ModelVM.Trainer;
using Gym.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .ForPath(a => a.User.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();

            CreateMap<AddUpdateSessionVM, Session>().ReverseMap();

            CreateMap<Session, GetSessionVM>().ReverseMap();
        }
    }
}
