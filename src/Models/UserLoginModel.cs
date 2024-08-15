
public class UserLoginModel
{
	[Required]
	[Display(Name = "User name")]
	public string Username { get; set; }

	[Required]
	[DataType(DataType.Password)]
	[Display(Name = "Password")]
	public string Password { get; set; }

	[Display(Name = "Remember me?")]
	public bool RememberMe { get; set; }

	public string ReturnUrl { get; set; }
}

