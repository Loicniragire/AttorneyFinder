namespace Attorneys.Services;

public class TokenProviderService : ITokenProvider
{
    private readonly IConfiguration _configuration;
	private readonly ILogger<TokenProviderService> _logger;

    public TokenProviderService(IConfiguration configuration, ILogger<TokenProviderService> logger)
    {
        _configuration = configuration;
		_logger = logger;
    }

    public string GenerateJwtToken(Attorney attorney)
    {
		ValidateAttorney(attorney);
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

		_logger.LogInformation($"Generating token for attorney {attorney.Username}");
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

	private void ValidateAttorney(Attorney attorney)
	{
		if (attorney == null || string.IsNullOrEmpty(attorney.Role))
		{
			_logger.LogError("Attorney is null or role is empty");
			throw new ArgumentNullException(nameof(attorney));
		}
	}

}

