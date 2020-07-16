using FreightManagement.Api.Infrastructure;
using FreightManagement.Api.Models;
using FreightManagement.Api.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreightManagement.Api.Application
{
    public class CreateCountryFreight : ICreateFreightStrategy
    {
        public Task<Result<Freight>> Create(CreateFreightViewModel viewModel)
        {
            var value = new FreightValue
            {
                Name = viewModel.Name,
                Price = viewModel.Price,
                BeginZipCode = "0",
                EndZipCode = "0",
                BeginWeight = 0,
                EndWeight = 0,
                Deadline = viewModel.Deadline
            };

            var values = new List<FreightValue>()
            {
                value
            };

            var freight = new Freight
            {
                Name = viewModel.Name,
                Values = values
            };

            var result = Result.Ok(freight);

            return Task.FromResult(result);
        }
    }
}