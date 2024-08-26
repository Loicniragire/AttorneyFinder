namespace Attorneys.Services;

public class TokenProviderService: ITokenProvider
{
	private readonly IConfiguration _configuration;

	public TokenProviderService(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string GenerateJwtToken(Attorney attorney)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[] { new Claim("id", attorney.Id.ToString()) }),
			Expires = DateTime.UtcNow.AddHours(1),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};
		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}

}

