namespace MicroServices.Playground.ServiceTwo.Api.Application.Queries;

public class GetPeople
{
    public record Query : IRequest<ICollection<Person>>;

    public record Person(Guid Id, string FirstName, string LastName);

    [UsedImplicitly]
    public class Handler : IRequestHandler<Query, ICollection<Person>>
    {
        private readonly ApplicationDbContext _ctx;

        public Handler(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<ICollection<Person>> Handle(Query qry, CancellationToken cancellationToken)
        {
            var people = await _ctx.People
                .Select(x => new Person(x.Id, x.FirstName, x.LastName))
                .ToListAsync(cancellationToken);

            return people;
        }
    }
}
