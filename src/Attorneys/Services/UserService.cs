namespace Attorneys.Services;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;

    public UserService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Attorney> GetDefaultAttorneys()
    {
        return _configuration.GetSection("AttorneySettings:DefaultAttorneys").Get<List<Attorney>>();
    }
}
