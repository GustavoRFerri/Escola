using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Entities : DbContext
    {
        public DbSet<Flight> Flights => Set<Flight>();

        public Entities(DbContextOptions options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>().HasKey(f => f.Id);

            modelBuilder.Entity<Flight>().OwnsMany(f => f.BookingList);

            base.OnModelCreating(modelBuilder);
        }
    }
}
 