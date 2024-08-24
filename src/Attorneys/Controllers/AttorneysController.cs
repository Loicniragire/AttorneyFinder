[ApiController]
[Route("api/[controller]")]
public class AttorneysController : ControllerBase
{
    private readonly IAttorneyDataProvider _attorneyDataProvider;

    public AttorneysController(IAttorneyDataProvider attorneyDataProvider)
	{
		_attorneyDataProvider = attorneyDataProvider;
	}

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<Attorney>>> GetAttorneys()
    {
        var attorneys = await _attorneyDataProvider.GetAttorneys();
		return Ok(attorneys);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "User, Manager, Admin")]
    public async Task<ActionResult<Attorney>> GetAttorney(int id)
    {
		return await _attorneyDataProvider.GetAttorney(id);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Attorney>> PostAttorney(Attorney attorney)
    {
		await _attorneyDataProvider.PostAttorney(attorney);

        return CreatedAtAction(nameof(GetAttorney), new { id = attorney.Id }, attorney);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> PutAttorney(int id, Attorney attorney)
    {
		if (id != attorney.Id)
		{
			return BadRequest();
		}

		var updatedAttorney = await _attorneyDataProvider.PutAttorney(id, attorney);
		if(updatedAttorney == null)
		{
			return NotFound();
		}

		return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> DeleteAttorney(int id)
    {
		await _attorneyDataProvider.DeleteAttorney(id);
		return NoContent();
    }
}

