using AutoMapper;
using SportAdvisorAPI.Models;

namespace SportAdvisorAPI.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AddTrainingSpotDTO, TrainingSpot>().ReverseMap();
            
            CreateMap<User, RegisterUserDto>().ReverseMap();
        }
    }
}