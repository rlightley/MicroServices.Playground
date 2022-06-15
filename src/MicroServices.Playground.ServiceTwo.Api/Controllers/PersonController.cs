namespace MicroServices.Playground.ServiceTwo.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetPeople() => Ok(await _mediator.Send(new GetPeople.Query()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(Guid id) => Ok(await _mediator.Send(new GetPerson.Query(id)));
}
