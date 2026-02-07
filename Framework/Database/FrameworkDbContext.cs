using Framework.Models;
using Framework.Models.Permissions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Framework.Database
{
    public class FrameworkDbContext(DbContextOptions options) : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<InboxMessage> InboxMessage { get; set; }
        
        public DbSet<Permission> Permission { get; set; }
        public DbSet<UserPermission> UserPermission { get; set; }
    }
}