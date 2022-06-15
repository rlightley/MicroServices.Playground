namespace MicroServices.Playground.ServiceOne.Api.Application.Commands;

public class CreatePerson
{
    public record Command(string FirstName, string LastName, string Email) : IRequest<Unit>;

    [UsedImplicitly]
    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICapPublisher _capPublisher;
        private readonly ApplicationDbContext _ctx;

        public Handler(ICapPublisher capPublisher, ApplicationDbContext ctx)
        {
            _capPublisher = capPublisher;
            _ctx = ctx;
        }

        public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
        {
            var person = new Person(command.FirstName, command.LastName, command.Email);
            await _ctx.People.AddAsync(person, cancellationToken);

            await _capPublisher.PublishAsync(nameof(PersonCreatedIntegrationMessage),
                new PersonCreatedIntegrationMessage(person.Id, person.FirstName, person.LastName, person.Email),
                cancellationToken: cancellationToken);

            return default;
        }
    }
}

