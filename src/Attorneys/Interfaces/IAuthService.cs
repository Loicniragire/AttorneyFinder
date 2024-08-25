namespace Attorneys.Interfaces;

public interface IAuthService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
}

