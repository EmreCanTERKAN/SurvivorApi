using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurvivorApi.Data;
using SurvivorApi.Entities;

[ApiController]
[Route("api/[controller]")]
public class CompetitorController : ControllerBase
{
    private readonly SurvivorContext _context;

    public CompetitorController(SurvivorContext context)
    {
        _context = context;
    }

    // GET: api/competitor
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Competitor>>> GetCompetitors()
    {
        return await _context.Competitors.Include(c => c.Category).ToListAsync();
    }

    // GET: api/competitor/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Competitor>> GetCompetitor(int id)
    {
        var competitor = await _context.Competitors.Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);

        if (competitor == null)
        {
            return NotFound();
        }

        return competitor;
    }

    // POST: api/competitor
    [HttpPost]
    public async Task<ActionResult<Competitor>> CreateCompetitor(Competitor competitor)
    {
        _context.Competitors.Add(competitor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCompetitor), new { id = competitor.Id }, competitor);
    }

    // PUT: api/competitor/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompetitor(int id, Competitor competitor)
    {
        if (id != competitor.Id)
        {
            return BadRequest();
        }

        _context.Entry(competitor).State = EntityState.Modified;
        competitor.ModifiedDate = DateTime.Now;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Competitors.Any(c => c.Id == id))
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

    // DELETE: api/competitor/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompetitor(int id)
    {
        var competitor = await _context.Competitors.FindAsync(id);
        if (competitor == null)
        {
            return NotFound();
        }

        competitor.IsDeleted = true;
        competitor.ModifiedDate = DateTime.Now;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}
