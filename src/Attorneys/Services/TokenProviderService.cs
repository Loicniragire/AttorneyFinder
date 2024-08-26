namespace Attorneys.Services;

public class TokenProviderService : ITokenProvider
{
    private readonly IConfiguration _configuration;

    public TokenProviderService(IConfiguration configuration)
    {
        _configuration = configuration;
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
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

	private void ValidateAttorney(Attorney attorney)
	{
		if (attorney == null || string.IsNullOrEmpty(attorney.Role))
		{
			throw new ArgumentNullException(nameof(attorney));
		}
	}

}

