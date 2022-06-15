namespace MicroServices.Playground.ServiceOne.Api.Application.Commands;

public class UpdatePerson
{
    public record Command : IRequest<Result>
    {
        public Guid Id { get; set; }
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
            var person = await _ctx.People
                .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (person == null)
            {
                throw new ArgumentException("Person not found");
            }

            person.Update(command.FirstName, command.LastName, command.Email);
            await _ctx.SaveChangesAsync(cancellationToken);

            await _capPublisher.PublishAsync(nameof(PersonUpdatedIntegrationMessage),
                new PersonUpdatedIntegrationMessage(person.Id,
                    command.FirstName, command.LastName, command.Email),
                cancellationToken: cancellationToken);

            return new Result
            {
                Id = person.Id
            };
        }
    }
}
