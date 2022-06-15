namespace MicroServices.Playground.ServiceTwo.Api.Application.IntegrationMessages.Subscribe;

public class PersonUpdatedIntegrationMessage
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}

[UsedImplicitly]
public class PersonUpdatedIntegrationMessageHandler : ICapSubscribe
{
    private readonly ApplicationDbContext _ctx;

    public PersonUpdatedIntegrationMessageHandler(ApplicationDbContext ctx) => _ctx = ctx;

    [UsedImplicitly]
    [CapSubscribe(nameof(PersonUpdatedIntegrationMessage))]
    public async Task Handle(PersonUpdatedIntegrationMessage msg)
    {
        var person = await _ctx.People.FindAsync(msg.Id);
        person?.Update(msg.FirstName, msg.LastName);
        await _ctx.SaveChangesAsync();
    }
}