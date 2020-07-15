using FreightManagement.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreightManagement.Api.Repositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> FindAll(int stateId);
    }
}