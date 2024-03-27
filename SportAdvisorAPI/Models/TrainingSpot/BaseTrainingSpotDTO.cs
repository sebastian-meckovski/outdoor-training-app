namespace SportAdvisorAPI.Models
{
    public class BaseTrainingSpotDTO
    {
        public required int PosX { get; set; }
        public required int PosY { get; set; }
        public string? Description { get; set; }
    }
}