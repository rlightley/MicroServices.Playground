namespace MicroServices.Playground.ServiceTwo.Api.Application.IntegrationMessages.Subscribe;

public class PersonDeletedIntegrationMessage
{
    public Guid Id { get; set; }
}

[UsedImplicitly]
public class PersonDeletedIntegrationMessageHandler : ICapSubscribe
{
    private readonly ApplicationDbContext _ctx;

    public PersonDeletedIntegrationMessageHandler(ApplicationDbContext ctx) => _ctx = ctx;

    [UsedImplicitly]
    [CapSubscribe(nameof(PersonDeletedIntegrationMessage))]
    public async Task Handle(PersonDeletedIntegrationMessage msg)
    {
        var person = await _ctx.People.FindAsync(msg.Id);
        person?.Delete();
        await _ctx.SaveChangesAsync();
    }
}