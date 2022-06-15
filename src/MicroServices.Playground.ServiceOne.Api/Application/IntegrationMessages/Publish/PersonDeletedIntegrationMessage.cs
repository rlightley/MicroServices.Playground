namespace MicroServices.Playground.ServiceOne.Api.Application.IntegrationMessages.Publish;

public class PersonDeletedIntegrationMessage
{
    public PersonDeletedIntegrationMessage(Guid id) => Id = id;

    public Guid Id { get; init; }
}
