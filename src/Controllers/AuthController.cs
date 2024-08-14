[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet("google-login")]
    public IActionResult GoogleLogin()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("google-response")]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (!result.Succeeded)
            return BadRequest();

        var claims = result.Principal.Identities
            .FirstOrDefault()?.Claims.Select(claim => new
            {
                claim.Type,
                claim.Value
            });

        return Ok(claims);
    }

    [HttpGet("apple-login")]
    public IActionResult AppleLogin()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("AppleResponse") };
        return Challenge(properties, "Apple");
    }

    [HttpGet("apple-response")]
    public async Task<IActionResult> AppleResponse()
    {
        var result = await HttpContext.AuthenticateAsync("Apple");
        if (!result.Succeeded)
            return BadRequest();

        var claims = result.Principal.Identities
            .FirstOrDefault()?.Claims.Select(claim => new
            {
                claim.Type,
                claim.Value
            });

        return Ok(claims);
    }
}

