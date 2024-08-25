namespace Attorneys.Services;

public class AuthService : IAuthService
{
    private readonly IAttorneyDataProvider _attorneyDataProvider;
	private readonly IUserService _userProviderService;
	private readonly IConfiguration _configuration;

    public AuthService(IUserService userService, IAttorneyDataProvider attorneyDataProvider, IConfiguration configuration)
    {
		_userProviderService = userService;
        _attorneyDataProvider = attorneyDataProvider;
		_configuration = configuration;
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

        var token = GenerateJwtToken(attorney);
        return new AuthenticateResponse { Token = token };
    }

    private string GenerateJwtToken(Attorney attorney)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", attorney.Id.ToString()),
                new Claim(ClaimTypes.Role, attorney.Role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpiryMinutes"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

public class MissingAttorneyException : Exception
{
	public MissingAttorneyException() : base("Attorney not found")
	{
	}
}

