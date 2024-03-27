using SportAdvisorAPI.Contracts;
using SportAdvisorAPI.Data;
using SportAdvisorAPI.Models;

namespace SportAdvisorAPI.Repository
{
    public class TrainingSpotsRepository : GenericRepository<TrainingSpot>, ITrainingSpotsRepository
    {
        private readonly SportAdvisorDbContext _context;
        public TrainingSpotsRepository(SportAdvisorDbContext context) : base(context)
        {
            _context = context;
        }
    }
}