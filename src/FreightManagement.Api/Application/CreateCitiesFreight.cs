using FreightManagement.Api.Infrastructure;
using FreightManagement.Api.Models;
using FreightManagement.Api.Repositories;
using FreightManagement.Api.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace FreightManagement.Api.Application
{
    public class CreateCitiesFreight : ICreateFreightStrategy
    {
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;

        public CreateCitiesFreight(IStateRepository stateRepository, ICityRepository cityRepository)
        {
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
        }

        public async Task<Result<Freight>> Create(CreateFreightViewModel viewModel)
        {
            var maybeState = await _stateRepository.FindById(viewModel.StateId);

            if (maybeState.HasNoValue)
            {
                return Result.Fail<Freight>("Estado não existe");
            }

            var state = maybeState.Value;

            var tasks = viewModel.CitiesId.Select(async it =>
            {
                var maybeCity = await _cityRepository.FindById(viewModel.StateId, it);

                if (maybeCity.HasNoValue)
                {
                    return Result.Fail<FreightValue>("Cidade não existe");
                }

                var city = maybeCity.Value;

                var value = new FreightValue
                {
                    Name = $"{viewModel.Name} {state.Name} {city.Name}",
                    Price = viewModel.Price,
                    BeginZipCode = city.BeginZipCode,
                    EndZipCode = city.EndZipCode,
                    BeginWeight = 0,
                    EndWeight = 0,
                    Deadline = viewModel.Deadline
                };

                return Result.Ok(value);
            });

            var results = await Task.WhenAll(tasks);

            var result = Result.Combine(results);

            if (result.Failure)
            {
                return Result.Fail<Freight>(result.Message);
            }

            var values = results.Select(it => it.Value);

            var freigth = new Freight
            {
                Name = viewModel.Name,
                Values = values
            };

            return Result.Ok(freigth);
        }
    }
}