namespace Attorneys.Services;

public class AuthService : IAuthService
{
    private readonly IAttorneyDataProvider _attorneyDataProvider;
	private readonly IUserService _userProviderService;
	private readonly ITokenProvider _tokenProvider;

    public AuthService(IUserService userService, IAttorneyDataProvider attorneyDataProvider, ITokenProvider tokenProvider)
    {
		_userProviderService = userService;
        _attorneyDataProvider = attorneyDataProvider;
		_tokenProvider = tokenProvider;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        var _defaultAttorneys = _userProviderService.GetDefaultAttorneys();
        var existingAttorneys = await _attorneyDataProvider.GetAttorneys();
        IEnumerable<Attorney> attorneyCollection = (existingAttorneys.Any()) ? existingAttorneys : _defaultAttorneys;
        var attorney = attorneyCollection.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        if (attorney == null)
        {
            throw new MissingAttorneyException();
        }

        var token = _tokenProvider.GenerateJwtToken(attorney);
        return new AuthenticateResponse { Token = token };
    }
}

public class MissingAttorneyException : Exception
{
	public MissingAttorneyException() : base("Attorney not found")
	{
	}
}

