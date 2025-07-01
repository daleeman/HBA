using HBA.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HBA.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Property> Property { get; set; }  // <-- Add this DbSet property
        public DbSet<PropertyType> PropertyType { get; set; } 
        public DbSet<CommissionSetup> CommissionSetup { get; set; } 
    }
}
