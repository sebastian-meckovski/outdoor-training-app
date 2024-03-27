using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportAdvisorAPI.Contracts;
using SportAdvisorAPI.Data;
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
        public async Task<ActionResult<IEnumerable<TrainingSpot>>> GetTrainingSpots()
        {
            if (_context.TrainingSpots == null)
            {
                return NotFound();
            }
            return await _context.TrainingSpots.ToListAsync();
        }

        // GET: api/TrainingSpots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingSpot>> GetTrainingSpot(Guid id)
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

            return trainingSpot;
        }

        // PUT: api/TrainingSpots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingSpot(Guid id, TrainingSpot trainingSpot)
        {
            if (id != trainingSpot.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainingSpot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingSpotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TrainingSpots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrainingSpot>> PostTrainingSpot(TrainingSpot trainingSpot)
        {
            if (_context.TrainingSpots == null)
            {
                return Problem("Entity set 'SportAdvisorDbContext.TrainingSpots'  is null.");
            }
            _context.TrainingSpots.Add(trainingSpot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingSpot", new { id = trainingSpot.Id }, trainingSpot);
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
