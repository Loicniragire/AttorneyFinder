namespace Attorneys.Tests;

[TestFixture]
public class AuthServiceTests
{
	private readonly Mock<IAttorneyDataProvider> _attorneyDataProviderMock;
	private readonly Mock<ITokenProvider> _tokenProviderMock;
	private AuthService _authService;
	private readonly List<Attorney> _defaultAttorneys = new List<Attorney>
	{
		new Attorney { Username = "admin", Password = "admin", Role = "Admin" },
		new Attorney { Username = "user", Password = "user", Role = "User" }
	};

	private readonly Mock<IUserService> _userServiceMock;

	public AuthServiceTests()
	{
		_tokenProviderMock = new Mock<ITokenProvider>();
		_attorneyDataProviderMock = new Mock<IAttorneyDataProvider>();
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
		_tokenProviderMock.Setup(x => x.GenerateJwtToken(It.IsAny<Attorney>())).Returns("token");

		_authService = new AuthService(_userServiceMock.Object, _attorneyDataProviderMock.Object, _tokenProviderMock.Object);
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

		_authService = new AuthService(_userServiceMock.Object, _attorneyDataProviderMock.Object, _tokenProviderMock.Object);
		var request = new AuthenticateRequest { Username = "admin", Password = "admin" };
		Assert.ThrowsAsync<MissingAttorneyException>(() => _authService.Authenticate(request));
	}

}
