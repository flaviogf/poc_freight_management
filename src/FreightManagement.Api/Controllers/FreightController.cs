using FreightManagement.Api.Application;
using FreightManagement.Api.Infrastructure;
using FreightManagement.Api.Repositories;
using FreightManagement.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace FreightManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FreightController : ControllerBase
    {
        private readonly IFreightApplication _freightApplication;
        private readonly IFreightRepository _freightRepository;
        private readonly IUnitOfWork _uow;

        public FreightController
        (
            IFreightApplication freightApplication,
            IFreightRepository freightRepository,
            IUnitOfWork uow
        )
        {
            _freightApplication = freightApplication;
            _freightRepository = freightRepository;
            _uow = uow;
        }

        [HttpGet]
        [SwaggerOperation(Tags = new string[] { "Freight" })]
        public async Task<IActionResult> Index()
        {
            var freights = await _freightRepository.FindAll();

            return Ok(freights);
        }

        [HttpPost]
        [SwaggerOperation(Tags = new string[] { "Freight" })]
        public async Task<IActionResult> Create([FromBody] CreateFreightViewModel viewModel, [FromServices] ICreateFreightStrategyFactory strategyFactory)
        {
            var strategy = strategyFactory.Create(viewModel.Region);

            var result = await _freightApplication.Create(viewModel, strategy);

            if (result.Failure)
            {
                _uow.Rollback();

                return BadRequest(result.Message);
            }

            _uow.Commit();

            return Ok();
        }
    }
}