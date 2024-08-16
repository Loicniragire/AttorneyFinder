public class AuthService : IAuthService
{
    private readonly List<Attorney> _attorneys;

    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
		_attorneys = configuration.GetSection("AttorneySettings:Attorneys").Get<List<Attorney>>();
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var attorney = _attorneys.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        if (attorney == null) return null;

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

