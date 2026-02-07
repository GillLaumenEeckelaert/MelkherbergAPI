using Microsoft.EntityFrameworkCore;
using Models.General;
using Models.Shows;

namespace Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Location> Location { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<ArtistPerson> ArtistPerson { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventArtist> EventArtist { get; set; }
        public DbSet<Show> Show { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
    }
}