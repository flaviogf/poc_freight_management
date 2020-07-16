using FreightManagement.Api.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FreightManagement.Api.Application
{
    public class CreateFreightStrategyFactory : ICreateFreightStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CreateFreightStrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICreateFreightStrategy Create(EFreightRegion region)
        {
            return region switch
            {
                EFreightRegion.Country => _serviceProvider.GetService<CreateCountryFreight>(),
                EFreightRegion.State => _serviceProvider.GetService<CreateStateFreight>(),
                EFreightRegion.City => _serviceProvider.GetService<CreateCitiesFreight>(),
                _ => throw new InvalidOperationException()
            };
        }
    }
}