using FreightManagement.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace FreightManagement.Api.Controllers
{
    [ApiController]
    [Route("State/{stateId}/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet]
        [SwaggerOperation(Tags = new string[] { "State" })]
        public async Task<IActionResult> Index([FromRoute] int stateId)
        {
            var cities = await _cityRepository.FindAll(stateId);

            return Ok(cities);
        }
    }
}