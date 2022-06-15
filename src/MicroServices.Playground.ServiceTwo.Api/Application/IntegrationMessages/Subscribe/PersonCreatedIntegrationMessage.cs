namespace MicroServices.Playground.ServiceTwo.Api.Application.IntegrationMessages.Subscribe;

public class PersonCreatedIntegrationMessage
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}

[UsedImplicitly]
public class PersonCreatedIntegrationMessageHandler : ICapSubscribe
{
    private readonly ApplicationDbContext _ctx;

    public PersonCreatedIntegrationMessageHandler(ApplicationDbContext ctx) => _ctx = ctx;

    [UsedImplicitly]
    [CapSubscribe(nameof(PersonCreatedIntegrationMessage))]
    public async Task Handle(PersonCreatedIntegrationMessage msg)
    {
        await _ctx.People.AddAsync(new Person(msg.Id, msg.FirstName, msg.LastName));
        await _ctx.SaveChangesAsync();
    }
}
