using JetBrains.Annotations;
using Refit;

namespace MicroServices.Playground.ServiceThree.Api.Infrastructure.ApiClients;

public interface IServiceTwoApiClient
{
    [Get("/person/{id}")]
    Task<Person> GetPerson(Guid id);

    [Get("/person")]
    Task<List<Person>> GetPeople();
}

public class Person
{
    [UsedImplicitly]
    public Guid Id { get; set; }
    [UsedImplicitly]
    public string? FirstName { get; set; }
    [UsedImplicitly]
    public string? LastName { get; set; }
}