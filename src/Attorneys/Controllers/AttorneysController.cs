[ApiController]
[Route("api/[controller]")]
public class AttorneysController : ControllerBase
{
    private readonly IAttorneyDataProvider _attorneyDataProvider;
	private readonly ILogger<AttorneysController> _logger;

    public AttorneysController(IAttorneyDataProvider attorneyDataProvider, ILogger<AttorneysController> logger)
	{
		_attorneyDataProvider = attorneyDataProvider;
		_logger = logger;
	}

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<Attorney>>> GetAttorneys()
    {
		_logger.LogInformation("Getting all attorneys");
        var attorneys = await _attorneyDataProvider.GetAttorneys();
		return Ok(attorneys);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "User, Manager, Admin")]
    public async Task<ActionResult<Attorney>> GetAttorney(int id)
    {
		_logger.LogInformation($"Getting attorney with id {id}");
		return await _attorneyDataProvider.GetAttorney(id);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Attorney>> PostAttorney(Attorney attorney)
    {
		_logger.LogInformation($"Creating attorney {attorney.Username}");
		await _attorneyDataProvider.PostAttorney(attorney);

        return CreatedAtAction(nameof(GetAttorney), new { id = attorney.Id }, attorney);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> PutAttorney(int id, Attorney attorney)
    {
		_logger.LogInformation($"Updating attorney with id {id}");
		if (id != attorney.Id)
		{
			return BadRequest();
		}

		var updatedAttorney = await _attorneyDataProvider.PutAttorney(id, attorney);
		if(updatedAttorney == null)
		{
			_logger.LogError($"Attorney with id {id} not found");
			return NotFound();
		}

		return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> DeleteAttorney(int id)
    {
		_logger.LogInformation($"Deleting attorney with id {id}");
		await _attorneyDataProvider.DeleteAttorney(id);
		return NoContent();
    }
}

