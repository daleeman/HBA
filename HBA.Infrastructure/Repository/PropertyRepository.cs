using HBA.Data.Model;
using HBA.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace HBA.Infrastructure.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext _context;

        public PropertyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _context.Property.Include(x=>x.PropertyType).ToListAsync();
        }

        public async Task<Property?> GetByIdAsync(int id)
        {
            return await _context.Property.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Property property)
        {
            await _context.Property.AddAsync(property);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Property property)
        {
            _context.Property.Update(property);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var property = await _context.Property.FindAsync(id);
            if (property != null)
            {
                _context.Property.Remove(property);
                await _context.SaveChangesAsync();
            }
        }
    }

}
