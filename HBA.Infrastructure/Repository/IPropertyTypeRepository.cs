using HBA.Data.Model;

namespace HBA.Infrastructure.Repository
{
    public interface IPropertyTypeRepository
    {
        Task<IEnumerable<PropertyType>> GetAllAsync();
    }
}
