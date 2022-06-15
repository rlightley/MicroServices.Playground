namespace MicroServices.Playground.ServiceOne.Api.Infrastructure.DataAccess;

public class PersonDbConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(x => x.FirstName)
            .HasMaxLength(32);

        builder.Property(x => x.LastName)
            .HasMaxLength(32);

        builder.Property(x => x.Email)
            .HasMaxLength(32);
    }
}
