using Microsoft.EntityFrameworkCore;
using TestAPI.Models;

namespace Database
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        // Add Test Models
        public DbSet<TestObject> TestObject { get; set; }
        
        // Add Identification Models
        //public DbSet<PublicLog> PublicLogs { get; set; }
    }
}