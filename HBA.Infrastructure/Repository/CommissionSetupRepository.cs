using HBA.Data.Model;
using HBA.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace HBA.Infrastructure.Repository
{
    public class CommissionSetupRepository : ICommissionSetupRepository
    {
        private readonly ApplicationDbContext _context;

        public CommissionSetupRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CommissionSetup>> GetAllAsync()
        {
            return await _context.CommissionSetup.ToListAsync();
        }
    }
}
