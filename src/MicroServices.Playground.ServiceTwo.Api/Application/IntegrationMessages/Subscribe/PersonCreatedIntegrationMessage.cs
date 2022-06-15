namespace MicroServices.Playground.ServiceTwo.Api.Application.IntegrationMessages.Subscribe;

public class PersonCreatedIntegrationMessage
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class PersonCreatedIntegrationMessageHandler : ICapSubscribe
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
