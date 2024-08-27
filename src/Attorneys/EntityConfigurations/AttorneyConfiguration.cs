namespace Attorneys.EntityConfigurations;

public class AttorneyConfiguration : IEntityTypeConfiguration<Attorney>
{
    public void Configure(EntityTypeBuilder<Attorney> builder)
    {
        builder.ToTable("Attorneys");
        builder.HasKey(a => a.Id);
        builder.HasIndex(a => a.LawFirm);
        builder.Property(a => a.LawFirm).IsRequired();
        builder.Property(a => a.PracticeArea).IsRequired();

        // seed data for Attorneys
        builder.HasData(
         new Attorney
         {
             Id = 1,
             Name = "John Doe",
             Email = "john.doe@example.com",
             Username = "john.doe",
             Password = "password",
             Role = "Admin",
			 Phone = "123-456-7890",
             LawFirm = "Law Firm 1",
             PracticeArea = "Practice Area 1",
             CreatedAt = DateTime.Now,
         },
         new Attorney
         {
             Id = 2,
             Name = "Jane Smith",
             Email = "jane.smith@example.com",
             Username = "jane.smith",
             Password = "password",
             Role = "User",
			 Phone = "123-456-7890",
             LawFirm = "Law Firm 1",
             PracticeArea = "Practice Area 1",
             CreatedAt = DateTime.Now,
         });
    }
}

