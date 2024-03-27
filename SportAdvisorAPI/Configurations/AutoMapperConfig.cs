using AutoMapper;
using SportAdvisorAPI.Models;

namespace SportAdvisorAPI.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CreateTrainingSpotDTO, TrainingSpot>().ReverseMap();
            CreateMap<GetTrainingSpotDTO, TrainingSpot>().ReverseMap();
            CreateMap<UpdateTrainingSpotDTO, TrainingSpot>().ReverseMap();

            CreateMap<User, RegisterUserDto>().ReverseMap();
        }
    }
}