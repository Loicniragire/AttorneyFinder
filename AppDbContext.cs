public class AppDbContext : DbContext
{
    public DbSet<Attorney> Attorneys { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attorney>().ToTable("Attorneys");
    }
}

public class Attorney
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

