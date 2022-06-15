namespace MicroServices.Playground.ServiceOne.Api.Application.Commands;

public class DeletePerson
{
    public record Command(Guid Id) : IRequest<Unit>;

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
            var person = await _ctx.People
                .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (person == null)
            {
                throw new ArgumentException("Person not found");
            }

            person.Delete();
            await _ctx.SaveChangesAsync(cancellationToken);

            await _capPublisher.PublishAsync(nameof(PersonDeletedIntegrationMessage),
                new PersonDeletedIntegrationMessage(person.Id),
                cancellationToken: cancellationToken);

            return default;
        }
    }
}
