
public interface IAuthService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
}

