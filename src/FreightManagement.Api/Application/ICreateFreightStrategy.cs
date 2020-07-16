using FreightManagement.Api.Infrastructure;
using FreightManagement.Api.Models;
using FreightManagement.Api.ViewModels;
using System.Threading.Tasks;

namespace FreightManagement.Api.Application
{
    public interface ICreateFreightStrategy
    {
        Task<Result<Freight>> Create(CreateFreightViewModel viewModel);
    }
}