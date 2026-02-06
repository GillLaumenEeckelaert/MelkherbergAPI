using Microsoft.EntityFrameworkCore;
using Models.Test;

namespace Database
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        // Add Test Models
        public DbSet<TestObject> TestObject { get; set; }
    }
}