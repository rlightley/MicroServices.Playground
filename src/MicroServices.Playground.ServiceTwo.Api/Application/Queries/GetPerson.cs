namespace MicroServices.Playground.ServiceTwo.Api.Application.Queries;

public class GetPerson
{
    public record Query(Guid Id) : IRequest<Person>;

    public record Person(Guid Id, string FirstName, string LastName);

    [UsedImplicitly]
    public class Handler : IRequestHandler<Query, Person>
    {
        private readonly ApplicationDbContext _ctx;

        public Handler(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<Person> Handle(Query qry, CancellationToken cancellationToken)
        {
            var person = await _ctx.People
                .Select(x => new Person(x.Id, x.FirstName, x.LastName))
                .SingleOrDefaultAsync(x => x.Id == qry.Id, cancellationToken);

            if (person == null)
            {
                throw new Exception("Person not found");
            }

            return person;

        }
    }
}
