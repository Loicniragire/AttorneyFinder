namespace Attorneys.Interfaces;

public interface ITokenProvider
{
    string GenerateJwtToken(Attorney attorney);
}
