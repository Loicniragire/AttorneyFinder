namespace Attorneys.Tests;

public class AuthServiceTests
{
	private readonly Mock<IAttorneyDataProvider> _attorneyDataProviderMock;
	private readonly Mock<IConfiguration> _configurationMock;
	private AuthService _authService;
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

		// Mocking the configuration for Jwt:Key and Jwt:ExpiryMinutes
		// generate random key using openssl rand -base64 32 
        _configurationMock.Setup(x => x["Jwt:Key"]).Returns("2WSE7/HJQMqkjGre+5YU9xVKjFGUP7QHvfJ15QIGPRI=");
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
}
