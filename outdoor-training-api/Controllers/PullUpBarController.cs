using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutdoorTraining.Models;
using PullupBars.Data;
using PullupBars.Models;

namespace PullupBars.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PullUpBarController : Controller
    {
        private readonly PullupBarsAPIDbContext dbContext;

        public PullUpBarController(PullupBarsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPullUpBars()
        {
            return Ok(await dbContext.PullupBars.ToListAsync());
        }
        [Authorize]
        [HttpPost]
        async public Task<IActionResult> AddPullUpBar(AddPullUpBar addPullUpBar)
        {
            var userId = addPullUpBar.UserId;
            var user = await dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            var pullUpBar = new PullUpBar()
            {
                Id = Guid.NewGuid(),
                DateAdded = DateTime.Now,
                PosX = addPullUpBar.PosX,
                PosY = addPullUpBar.PosY,
                Description = addPullUpBar.Description,
                UserId = user.Id
            };

            await dbContext.PullupBars.AddAsync(pullUpBar);
            await dbContext.SaveChangesAsync();
            return Ok(pullUpBar);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatePullupBar([FromRoute] Guid id, UpdatePullupBar updatePullUpBar)
        {
            var pullupBar = await dbContext.PullupBars.FindAsync(id);

            if (pullupBar != null)
            {
                pullupBar.PosX = updatePullUpBar.PosX;
                pullupBar.PosY = updatePullUpBar.PosY;
                pullupBar.Description = updatePullUpBar.Description;

                await dbContext.SaveChangesAsync();

                return Ok(pullupBar);
            }

            return NotFound();
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetPullupBar([FromRoute] Guid id)
        {
            var pullUpBar = await dbContext.PullupBars.FindAsync(id);
            if (pullUpBar != null)
            {
                return Ok(pullUpBar);
            }

            return NotFound();
        }
        [HttpDelete]
        [Authorize]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletePullupBar([FromRoute] Guid id)
        {
            var pullUpBar = await dbContext.PullupBars.FindAsync(id);
            if (pullUpBar != null)
            {
                dbContext.Remove(pullUpBar);
                await dbContext.SaveChangesAsync();
                return Ok(pullUpBar);
            }

            return NotFound();
        }
    }
}
