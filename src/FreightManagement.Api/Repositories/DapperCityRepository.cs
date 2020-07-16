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
    public class DapperCityRepository : ICityRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DapperCityRepository> _logger;

        public DapperCityRepository(IUnitOfWork uow, ILogger<DapperCityRepository> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<IEnumerable<City>> FindAll(int stateId)
        {
            try
            {
                var procedure = "usp_Cidade_Sel_IdEstado";

                var param = new
                {
                    IdEstado = stateId
                };

                var cities = await _uow.Connection.QueryAsync<City>(procedure, param: param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);

                return cities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return new List<City>();
            }
        }

        public async Task<Maybe<City>> FindById(int stateId, int cityId)
        {
            try
            {
                var procedure = "usp_Cidade_Sel_IdCidade";

                var param = new
                {
                    IdEstado = stateId,
                    IdCidade = cityId
                };

                var city = await _uow.Connection.QuerySingleOrDefaultAsync<City>(procedure, param: param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);

                return city;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return null;
            }
        }
    }
}