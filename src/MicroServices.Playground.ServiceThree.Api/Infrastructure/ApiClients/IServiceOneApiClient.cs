using JetBrains.Annotations;
using Refit;

namespace MicroServices.Playground.ServiceThree.Api.Infrastructure.ApiClients;

public interface IServiceOneApiClient
{
    [Post("/person")]
    Task<Result> CreatePerson([Body] PersonCommand cmd);

    [Patch("/person")]
    Task<Result> UpdatePerson([Body] PersonCommand cmd);

    [Delete("/person/{id{")]
    Task<Result> DeletePerson(Guid id);
}


public class PersonCommand
{
    [UsedImplicitly]
    public string FirstName { get; set; }
    [UsedImplicitly]
    public string LastName { get; set; }
    [UsedImplicitly]
    public string Email { get; set; }
}

public class Result
{
    public Guid Id { get; set; }
}