using FreightManagement.Api.Infrastructure;
using FreightManagement.Api.Models;
using FreightManagement.Api.Repositories;
using FreightManagement.Api.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreightManagement.Api.Application
{
    public class CreateStateFreight : ICreateFreightStrategy
    {
        private readonly IStateRepository _stateRepository;

        public CreateStateFreight(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public async Task<Result<Freight>> Create(CreateFreightViewModel viewModel)
        {
            var maybeState = await _stateRepository.FindById(viewModel.StateId);

            if (maybeState.HasNoValue)
            {
                return Result.Fail<Freight>("Estado não existe");
            }

            var state = maybeState.Value;

            var value = new FreightValue
            {
                Name = $"{viewModel.Name} {state.Name}",
                Price = viewModel.Price,
                BeginZipCode = state.BeginZipCode,
                EndZipCode = state.EndZipCode,
                BeginWeight = 0,
                EndWeight = 0,
                Deadline = viewModel.Deadline
            };

            var values = new List<FreightValue>
            {
                value
            };

            var freight = new Freight
            {
                Name = viewModel.Name,
                Values = values
            };

            return Result.Ok(freight);
        }
    }
}