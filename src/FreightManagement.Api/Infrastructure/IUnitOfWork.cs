using System.Data;

namespace FreightManagement.Api.Infrastructure
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        void Commit();

        void Rollback();
    }
}