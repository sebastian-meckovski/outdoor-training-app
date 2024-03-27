using SportAdvisorAPI.Models;

namespace SportAdvisorAPI.Contracts
{
    public interface ITrainingSpotsRepository : IGenericRepository<TrainingSpot>
    {
        Task<GetTrainingSpotDTO?> AddAsync(CreateTrainingSpotDTO entity, string? token);

    }
}