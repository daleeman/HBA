using HBA.Data.Model;
using HBA.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace HBA.Infrastructure.Repository
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public PropertyTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PropertyType>> GetAllAsync()
        {
            return await _context.PropertyType.ToListAsync();
        }
    }
}
