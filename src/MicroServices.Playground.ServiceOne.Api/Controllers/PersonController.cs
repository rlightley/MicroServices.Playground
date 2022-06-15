namespace MicroServices.Playground.ServiceOne.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePerson.Command cmd) => Ok(await _mediator.Send(cmd));

    [HttpPatch]
    public async Task<IActionResult> UpdatePerson([FromBody] UpdatePerson.Command cmd) => Ok(await _mediator.Send(cmd));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(Guid id) => Ok(await _mediator.Send(new DeletePerson.Command(id)));
}
