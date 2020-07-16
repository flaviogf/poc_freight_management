using FreightManagement.Api.Enums;

namespace FreightManagement.Api.Application
{
    public interface ICreateFreightStrategyFactory
    {
        ICreateFreightStrategy Create(EFreightRegion region);
    }
}