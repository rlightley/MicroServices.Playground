using MicroServices.Playground.ServiceThree.Api.Infrastructure.ApiClients;
using Microsoft.AspNetCore.Mvc;

namespace MicroServices.Playground.ServiceThree.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IServiceOneApiClient _serviceOneApiClient;
    private readonly IServiceTwoApiClient _serviceTwoApiClient;

    public PersonController(IServiceOneApiClient serviceOneApiClient, IServiceTwoApiClient serviceTwoApiClient)
    {
        _serviceOneApiClient = serviceOneApiClient;
        _serviceTwoApiClient = serviceTwoApiClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetPeople() => Ok(await _serviceTwoApiClient.GetPeople());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(Guid id) => Ok(await _serviceTwoApiClient.GetPerson(id));

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] PersonCommand cmd) => Ok(await _serviceOneApiClient.CreatePerson(cmd));

    [HttpPatch]
    public async Task<IActionResult> UpdatePerson([FromBody] PersonCommand cmd) => Ok(await _serviceOneApiClient.UpdatePerson(cmd));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(Guid id) => Ok(await _serviceOneApiClient.DeletePerson(id));
}