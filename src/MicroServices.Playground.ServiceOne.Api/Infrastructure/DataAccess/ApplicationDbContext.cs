namespace MicroServices.Playground.ServiceOne.Api.Infrastructure.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonDbConfiguration());
    }

    public DbSet<Person> People { get; set; } = null!;
}
