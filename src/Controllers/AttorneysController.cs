[ApiController]
[Route("api/[controller]")]
public class AttorneysController : ControllerBase
{
    private readonly AppDbContext _context;

    public AttorneysController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
	[Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<Attorney>>> GetAttorneys()
    {
        return await _context.Attorneys.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Attorney>> GetAttorney(int id)
    {
        var attorney = await _context.Attorneys.FindAsync(id);

        if (attorney == null)
        {
            return NotFound();
        }

        return attorney;
    }

    [HttpPost]
    public async Task<ActionResult<Attorney>> PostAttorney(Attorney attorney)
    {
        _context.Attorneys.Add(attorney);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAttorney), new { id = attorney.Id }, attorney);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAttorney(int id, Attorney attorney)
    {
        if (id != attorney.Id)
        {
            return BadRequest();
        }

        _context.Entry(attorney).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AttorneyExists(id))
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttorney(int id)
    {
        var attorney = await _context.Attorneys.FindAsync(id);
        if (attorney == null)
        {
            return NotFound();
        }

        _context.Attorneys.Remove(attorney);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AttorneyExists(int id)
    {
        return _context.Attorneys.Any(e => e.Id == id);
    }
}

