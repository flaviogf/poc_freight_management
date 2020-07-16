using FreightManagement.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace FreightManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StateController : ControllerBase
    {
        private readonly IStateRepository _stateRepository;

        public StateController(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        [HttpGet]
        [SwaggerOperation(Tags = new string[] { "State" })]
        public async Task<IActionResult> Index()
        {
            var states = await _stateRepository.FindAll();

            return Ok(states);
        }
    }
}