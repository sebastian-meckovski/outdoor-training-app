namespace SportAdvisorAPI.Models
{
    public class UpdateTrainingSpotDTO : BaseTrainingSpotDTO
    {
        public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
    }
}