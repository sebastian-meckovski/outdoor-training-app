using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportAdvisorAPI.Contracts;
using SportAdvisorAPI.Data;
using SportAdvisorAPI.Helpers;
using SportAdvisorAPI.Models;

namespace SportAdvisorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingSpotsController : ControllerBase
    {
        private readonly SportAdvisorDbContext _context;
        private readonly IMapper _mapper;

        private readonly ITrainingSpotsRepository _trainingSpotsRepository;


        public TrainingSpotsController(SportAdvisorDbContext context, ITrainingSpotsRepository trainingSpotsRepository, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _trainingSpotsRepository = trainingSpotsRepository;
        }

        // GET: api/TrainingSpots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTrainingSpotDTO>>> GetTrainingSpots()
        {
            var trainingSpots = await _trainingSpotsRepository.GetAllAsync();
            var TrainingSpotsObjects = _mapper.Map<List<GetTrainingSpotDTO>>(trainingSpots);
            return Ok(TrainingSpotsObjects);
        }

        // GET: api/TrainingSpots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTrainingSpotDTO>> GetTrainingSpot(Guid id)
        {
            var trainingSpot = await _trainingSpotsRepository.GetAsync(id);
            if (trainingSpot is null)
            {
                return NotFound();
            }
            var trainingSpotObject = _mapper.Map<GetTrainingSpotDTO>(trainingSpot);

            return Ok(trainingSpotObject);
        }

        // PUT: api/TrainingSpots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutTrainingSpot(Guid id, UpdateTrainingSpotDTO updateTrainingSpotDTO)
        {
            string? token = await HttpContext.GetTokenAsync("access_token");
            string? userId = HelperFunctions.GetJWTTokenClaim(token, JwtRegisteredClaimNames.Sub);

            // protect user records from being updated by other users
            if (userId is null || updateTrainingSpotDTO.UserId.ToString() != userId)
            {
                return Unauthorized();
            }
            // disallow editing the id
            if (id != updateTrainingSpotDTO.Id)
            {
                return BadRequest();
            }

            // get the requested record
            var trainingSpotobject = await _trainingSpotsRepository.GetAsync(id);
            if (trainingSpotobject is null)
            {
                return BadRequest();
            }
            _mapper.Map(updateTrainingSpotDTO, trainingSpotobject);

            try
            {   
                await _trainingSpotsRepository.UpdateAsync(trainingSpotobject);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingSpotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }

            var trainingSpotToReturn = _mapper.Map<GetTrainingSpotDTO>(trainingSpotobject);

            return Ok(trainingSpotToReturn);
        }

        // POST: api/TrainingSpots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<GetTrainingSpotDTO>> PostTrainingSpot(CreateTrainingSpotDTO createTrainingSpotDTO)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var result = await _trainingSpotsRepository.AddAsync(createTrainingSpotDTO, token);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        // DELETE: api/TrainingSpots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingSpot(Guid id)
        {
            if (_context.TrainingSpots == null)
            {
                return NotFound();
            }
            var trainingSpot = await _context.TrainingSpots.FindAsync(id);
            if (trainingSpot == null)
            {
                return NotFound();
            }

            _context.TrainingSpots.Remove(trainingSpot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainingSpotExists(Guid id)
        {
            return (_context.TrainingSpots?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
