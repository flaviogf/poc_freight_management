using FreightManagement.Api.Infrastructure;
using FreightManagement.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreightManagement.Api.Repositories
{
    public interface IFreightRepository
    {
        Task<Result> Save(Freight freight);

        Task<IEnumerable<Freight>> FindAll();
    }
}