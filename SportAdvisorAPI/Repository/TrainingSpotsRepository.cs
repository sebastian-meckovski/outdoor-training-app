using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SportAdvisorAPI.Contracts;
using SportAdvisorAPI.Data;
using SportAdvisorAPI.Helpers;
using SportAdvisorAPI.Models;

namespace SportAdvisorAPI.Repository
{
    public class TrainingSpotsRepository : GenericRepository<TrainingSpot>, ITrainingSpotsRepository
    {
        private readonly SportAdvisorDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;


        public TrainingSpotsRepository(SportAdvisorDbContext context, IMapper mapper, UserManager<User> userManager) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;

        }

        public async Task<GetTrainingSpotDTO?> AddAsync(CreateTrainingSpotDTO createTrainingSpotDTO, string? token)
        {
            // Mapping DTO
            var newTrainingSpot = _mapper.Map<TrainingSpot>(createTrainingSpotDTO);

            // Getting user
            var userId = HelperFunctions.GetJWTTokenClaim(token, JwtRegisteredClaimNames.Sub);
            var user = userId is not null ? await _userManager.FindByIdAsync(userId) : null;

            if (userId is null || user is null)
            {
                return null;
            }
            // updating object
            newTrainingSpot.User = user;
            newTrainingSpot.DateAdded = DateTime.Now;

            await _context.AddAsync(newTrainingSpot);
            await _context.SaveChangesAsync();
            // Mapping return object and returning
            var trainingSpotToReturn = _mapper.Map<GetTrainingSpotDTO>(newTrainingSpot);
            return trainingSpotToReturn;
        }
    }
}