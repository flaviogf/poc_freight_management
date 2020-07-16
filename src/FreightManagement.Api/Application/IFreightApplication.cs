using FreightManagement.Api.Infrastructure;
using FreightManagement.Api.ViewModels;
using System.Threading.Tasks;

namespace FreightManagement.Api.Application
{
    public interface IFreightApplication
    {
        Task<Result> Create(CreateFreightViewModel viewModel, ICreateFreightStrategy strategy);
    }
}