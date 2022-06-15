namespace MicroServices.Playground.ServiceOne.Api.Application.Commands;

public class CreatePerson
{
    public record Command : IRequest<Result>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class Result
    {
        public Guid Id { get; set; }
    }

    [UsedImplicitly]
    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly ICapPublisher _capPublisher;
        private readonly ApplicationDbContext _ctx;

        public Handler(ICapPublisher capPublisher, ApplicationDbContext ctx)
        {
            _capPublisher = capPublisher;
            _ctx = ctx;
        }

        public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
        {
            var person = new Person(command.FirstName, command.LastName, command.Email);
            await _ctx.People.AddAsync(person, cancellationToken);

            await _capPublisher.PublishAsync(nameof(PersonCreatedIntegrationMessage),
                new PersonCreatedIntegrationMessage(person.Id, person.FirstName, person.LastName, person.Email),
                cancellationToken: cancellationToken);

            return new Result
            {
                Id = person.Id
            };
        }
    }
}

