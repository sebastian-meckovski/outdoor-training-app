using AutoMapper;
using SportAdvisorAPI.Models;

namespace SportAdvisorAPI.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AddTrainingSpotDTO, TrainingSpot>().ReverseMap();
            CreateMap<CreateTrainingSpotDTO, TrainingSpot>().ReverseMap();
            CreateMap<GetTrainingSpotDTO, TrainingSpot>().ReverseMap();
            
            CreateMap<User, RegisterUserDto>().ReverseMap();
        }
    }
}