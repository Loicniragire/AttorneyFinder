public class AppDbContext : DbContext
{
    public DbSet<Attorney> Attorneys { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AttorneyConfiguration());
    }
}
