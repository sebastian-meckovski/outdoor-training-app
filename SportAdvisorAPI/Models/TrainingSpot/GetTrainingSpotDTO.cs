namespace SportAdvisorAPI.Models
{
    public class GetTrainingSpotDTO : BaseTrainingSpotDTO
    {
        public required Guid Id { get; set; }
        public required DateTime DateAdded { get; set; }
        public required Guid UserId { get; set; }
    }
}