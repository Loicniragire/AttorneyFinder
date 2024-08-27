namespace Attorneys.Entities;

public class Attorney: User
{
	public int Id { get; set; }
	public string LawFirm { get; set; }
	public string PracticeArea { get; set; }
}
