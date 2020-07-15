using FreightManagement.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightManagement.Api.Controllers
{
    [Route("[controller]")]
    public class FreightController : Controller
    {
        private readonly IFreightRepository _freightRepository;

        public FreightController(IFreightRepository freightRepository)
        {
            _freightRepository = freightRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var freights = await _freightRepository.FindAll();

            return Ok(freights);
        }
    }
}