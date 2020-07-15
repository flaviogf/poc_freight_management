using Dapper;
using FreightManagement.Api.Infrastructure;
using FreightManagement.Api.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FreightManagement.Api.Repositories
{
    public class DapperFreightRepository : IFreightRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DapperFreightRepository> _logger;

        public DapperFreightRepository(IUnitOfWork uow, ILogger<DapperFreightRepository> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<IEnumerable<Freight>> FindAll()
        {
            try
            {
                var procedure = "usp_Frete_Sel_IdUsuario";

                var param = new
                {
                    IdUsuario = 863,
                    IdParceiro = 68
                };

                var freights = await _uow.Connection.QueryAsync<Freight>(procedure, param: param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);

                await Task.WhenAll(freights.Select(FindAll));

                return freights;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return new List<Freight>();
            }
        }

        private async Task<IEnumerable<FreightValue>> FindAll(Freight freight)
        {
            try
            {
                var procedure = "usp_FreteValor_Sel_IdFrete";

                var param = new
                {
                    IdFrete = freight.Id,
                };

                var values = await _uow.Connection.QueryAsync<FreightValue>(procedure, param: param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);

                freight.Values = values;

                return values;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return new List<FreightValue>();
            }
        }
    }
}