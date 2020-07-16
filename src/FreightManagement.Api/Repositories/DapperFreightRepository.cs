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

        public async Task<Result> Save(Freight freight)
        {
            try
            {
                var procedure = "usp_Frete_Ins";

                var param = new
                {
                    IdUsuario = 863,
                    Nome = freight.Name
                };

                var freightId = await _uow.Connection.QuerySingleOrDefaultAsync<int>(procedure, param: param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);

                freight.Id = freightId;

                var tasks = freight.Values.Select(async it =>
                {
                    var procedure = "usp_FreteValor_Ins";

                    var param = new
                    {
                        IdFrete = freightId,
                        Nome = it.Name,
                        Valor = it.Price,
                        CEPInicio = it.BeginZipCode,
                        CEPFim = it.EndZipCode,
                        PesoInicio = it.BeginWeight,
                        PesoFim = it.EndWeight,
                        Prazo = it.Deadline
                    };

                    var valueId = await _uow.Connection.QuerySingleOrDefaultAsync<int>(procedure, param: param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);

                    it.Id = valueId;

                    return valueId;
                });

                await Task.WhenAll(tasks);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return Result.Fail("Falha ao salvar frete");
            }
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

                var tasks = freights.Select(async it =>
                {
                    var procedure = "usp_FreteValor_Sel_IdFrete";

                    var param = new
                    {
                        IdFrete = it.Id,
                    };

                    var values = await _uow.Connection.QueryAsync<FreightValue>(procedure, param: param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);

                    it.Values = values;

                    return values;
                });

                await Task.WhenAll(tasks);

                return freights;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return new List<Freight>();
            }
        }
    }
}