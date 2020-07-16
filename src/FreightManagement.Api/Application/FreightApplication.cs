using FreightManagement.Api.Infrastructure;
using FreightManagement.Api.Repositories;
using FreightManagement.Api.ViewModels;
using System.Threading.Tasks;

namespace FreightManagement.Api.Application
{
    public class FreightApplication : IFreightApplication
    {
        private readonly IFreightRepository _freightRepository;

        public FreightApplication(IFreightRepository freightRepository)
        {
            _freightRepository = freightRepository;
        }

        public async Task<Result> Create(CreateFreightViewModel viewModel, ICreateFreightStrategy strategy)
        {
            var freightResult = await strategy.Create(viewModel);

            if (freightResult.Failure)
            {
                return Result.Fail(freightResult.Message);
            }

            var saveResult = await _freightRepository.Save(freightResult.Value);

            return saveResult;
        }
    }
}