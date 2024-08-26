namespace Attorneys.Tests;

public class AuthServiceTests
{
	private readonly Mock<IAttorneyDataProvider> _attorneyDataProviderMock;
	private readonly Mock<IConfiguration> _configurationMock;
	private AuthService _authService;
	private const string _securityKey = "2WSE7/HJQMqkjGre+5YU9xVKjFGUP7QHvfJ15QIGPRI=";
	private readonly List<Attorney> _defaultAttorneys = new List<Attorney>
	{
		new Attorney { Username = "admin", Password = "admin", Role = "Admin" },
		new Attorney { Username = "user", Password = "user", Role = "User" }
	};

	private readonly Mock<IUserService> _userServiceMock;

	public AuthServiceTests()
	{
		_attorneyDataProviderMock = new Mock<IAttorneyDataProvider>();
		_configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(x => x["Jwt:Key"]).Returns(_securityKey);
        _configurationMock.Setup(x => x["Jwt:ExpiryMinutes"]).Returns("60");
		_userServiceMock = new Mock<IUserService>();
	}

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task ShouldAuthenticateAgainstDefaultUsersWhenNoUsersInDatabase()
    {
		_userServiceMock.Setup(x => x.GetDefaultAttorneys()).Returns(_defaultAttorneys);
		_attorneyDataProviderMock.Setup(x => x.GetAttorneys()).ReturnsAsync(new List<Attorney>());

		_authService = new AuthService(_userServiceMock.Object, _attorneyDataProviderMock.Object, _configurationMock.Object);
		var request = new AuthenticateRequest { Username = "admin", Password = "admin" };
		AuthenticateResponse response = await _authService.Authenticate(request);
		Assert.That(response, Is.Not.Null);
		Assert.That(response.Token, Is.Not.Null);
    }

	[Test]
	public async Task ShouldThrowMissingAttorneyExceptionWhenDefaultAttorneysAndDatabaseAreEmpty()
	{
		_userServiceMock.Setup(x => x.GetDefaultAttorneys()).Returns(new List<Attorney>());
		_attorneyDataProviderMock.Setup(x => x.GetAttorneys()).ReturnsAsync(new List<Attorney>());

		_authService = new AuthService(_userServiceMock.Object, _attorneyDataProviderMock.Object, _configurationMock.Object);
		var request = new AuthenticateRequest { Username = "admin", Password = "admin" };
		Assert.ThrowsAsync<MissingAttorneyException>(() => _authService.Authenticate(request));
	}

	[Test]
	public async Task ShouldGenerateJwtTokenWithClaims()
	{
		_userServiceMock.Setup(x => x.GetDefaultAttorneys()).Returns(_defaultAttorneys);
		_attorneyDataProviderMock.Setup(x => x.GetAttorneys()).ReturnsAsync(new List<Attorney>());

		_authService = new AuthService(_userServiceMock.Object, _attorneyDataProviderMock.Object, _configurationMock.Object);
		var request = new AuthenticateRequest { Username = "admin", Password = "admin" };
		AuthenticateResponse response = await _authService.Authenticate(request);
		Assert.That(response, Is.Not.Null);
		Assert.That(response.Token, Is.Not.Null);

		// Given response token, decode it and check claims
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(_securityKey);
		var tokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(key),
			ValidateIssuer = false,
			ValidateAudience = false,
			RequireExpirationTime = false,
			ValidateLifetime = false
		};

		var principal = tokenHandler.ValidateToken(response.Token, tokenValidationParameters, out var validatedToken);

		Assert.That(validatedToken is JwtSecurityToken);

		var claims = principal.Claims.ToList();

		// Assert that the role claim exists and its value is "Admin"
		var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
		Assert.That(roleClaim, Is.Not.Null);
		Assert.That(roleClaim.Value, Is.EqualTo("Admin"));
	}
}
