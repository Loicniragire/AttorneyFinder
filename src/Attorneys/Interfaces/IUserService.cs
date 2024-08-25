namespace Attorneys.Interfaces;

public interface IUserService
{
	IEnumerable<Attorney> GetDefaultAttorneys();
}

