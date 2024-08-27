using Microsoft.Extensions.Logging;

namespace Attorneys.Tests;

[TestFixture]
public class TokenProviderServiceTests
{
	private readonly Mock<IConfiguration> _configurationMock;
	private readonly Mock<ILogger<TokenProviderService>> _loggerMock;
	private const string _securityKey = "2WSE7/HJQMqkjGre+5YU9xVKjFGUP7QHvfJ15QIGPRI=";

	public TokenProviderServiceTests()
	{
		_configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(x => x["Jwt:Key"]).Returns(_securityKey);
        _configurationMock.Setup(x => x["Jwt:ExpiryMinutes"]).Returns("60");
		_loggerMock = new Mock<ILogger<TokenProviderService>>();
	}

    [Test]
    public void ShouldGenerateJwtTokenWithClaims()
    {
		var tokenProviderService = new TokenProviderService(_configurationMock.Object, _loggerMock.Object);
		var attorney = new Attorney { Id = 1, Role = "Admin" };
		var token = tokenProviderService.GenerateJwtToken(attorney);

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

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
        Assert.That(validatedToken is JwtSecurityToken);

        var claims = principal.Claims.ToList();

        // Assert that the role claim exists and its value is "Admin"
        var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        Assert.That(roleClaim, Is.Not.Null);
        Assert.That(roleClaim.Value, Is.EqualTo("Admin"));
    }

	[Test]
	public void ShouldThrowArgumentNullExceptionWhenAttorneyRoleIsNullOrEmpty()
	{
		var tokenProviderService = new TokenProviderService(_configurationMock.Object, _loggerMock.Object);
		Assert.Throws<ArgumentNullException>(() => tokenProviderService.GenerateJwtToken(null));
	}
}

