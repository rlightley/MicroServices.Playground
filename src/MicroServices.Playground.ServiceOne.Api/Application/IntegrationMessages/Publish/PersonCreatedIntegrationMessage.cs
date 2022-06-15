namespace MicroServices.Playground.ServiceOne.Api.Application.IntegrationMessages.Publish;

public class PersonCreatedIntegrationMessage
{
    public PersonCreatedIntegrationMessage(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
}
