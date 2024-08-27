namespace Attorneys.Entities;

public class User
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	public string Role { get; set; }
	public string Phone { get; set; }
	public bool IsDeleted { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }
}
