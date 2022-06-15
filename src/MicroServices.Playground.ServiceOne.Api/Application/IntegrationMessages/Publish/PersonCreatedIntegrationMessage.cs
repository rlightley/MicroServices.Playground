namespace MicroServices.Playground.ServiceOne.Api.Application.IntegrationMessages.Publish;

public class PersonCreatedIntegrationMessage
{
    public PersonCreatedIntegrationMessage(Guid id, string firstName, string lastName, string email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    
    public Guid Id { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
}
