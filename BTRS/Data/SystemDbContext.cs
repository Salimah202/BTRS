using BTRS.Models;
using Microsoft.EntityFrameworkCore;

namespace BTRS.Data
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Trip> trip { get; set; }
        public DbSet<Passengers> passengers { get; set; }
        public DbSet<Passenger_Trip> passenger_Trip { get; set; }
        public DbSet<Bus> Bus { get; set; }
        public DbSet<Administrators> administrators { get; set; }
    }
}
