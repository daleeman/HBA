using HBA.Data.Model;

namespace HBA.Infrastructure.Repository
{
    public interface ICommissionSetupRepository
    {
        Task<IEnumerable<CommissionSetup>> GetAllAsync();
    }
}
