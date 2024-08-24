public class AttorneyDataRepository: IAttorneyDataProvider
{
	private readonly AppDbContext _context;

	public AttorneyDataRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Attorney>> GetAttorneys()
	{
		return await _context.Attorneys.ToListAsync();
	}

	public async Task<Attorney> GetAttorney(int id)
	{
		return await _context.Attorneys.FindAsync(id);
	}

	public async Task<Attorney> PostAttorney(Attorney attorney)
	{
		_context.Attorneys.Add(attorney);
		await _context.SaveChangesAsync();

		return attorney;
	}

	public async Task<Attorney> PutAttorney(int id, Attorney attorney)
	{
		if (id != attorney.Id)
		{
			return null;
		}

		_context.Entry(attorney).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			return null;
		}

		return attorney;
	}

	public async Task<Attorney> DeleteAttorney(int id)
	{
		var attorney = await _context.Attorneys.FindAsync(id);
		if (attorney == null)
		{
			return null;
		}

		_context.Attorneys.Remove(attorney);
		await _context.SaveChangesAsync();

		return attorney;
	}

	private bool AttorneyExists(int id)
	{
		return _context.Attorneys.Any(e => e.Id == id);
	}
}

