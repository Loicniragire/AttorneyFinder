public interface IAttorneyDataProvider
{
	Task<IEnumerable<Attorney>> GetAttorneys();
	Task<Attorney> GetAttorney(int id);
	Task<Attorney> PostAttorney(Attorney attorney);
	Task<Attorney> PutAttorney(int id, Attorney attorney);
	Task<Attorney> DeleteAttorney(int id);
}
