using Dapper;
using FreightManagement.Api.Infrastructure;
using FreightManagement.Api.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FreightManagement.Api.Repositories
{
    public class DapperStateRepository : IStateRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DapperStateRepository> _logger;

        public DapperStateRepository(IUnitOfWork uow, ILogger<DapperStateRepository> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<IEnumerable<State>> FindAll()
        {
            try
            {
                var procedure = "usp_Estado_Sel";

                var cities = await _uow.Connection.QueryAsync<State>(procedure, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);

                return cities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return new List<State>();
            }
        }

        public async Task<Maybe<State>> FindById(int id)
        {
            try
            {
                var procedure = "usp_Estado_Sel_IdEstado";

                var param = new
                {
                    IdEstado = id
                };

                var state = await _uow.Connection.QuerySingleOrDefaultAsync<State>(procedure, param: param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);

                return state;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return null;
            }
        }
    }
}