using FreightManagement.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreightManagement.Api.Repositories
{
    public interface IStateRepository
    {
        Task<IEnumerable<State>> FindAll();
    }
}