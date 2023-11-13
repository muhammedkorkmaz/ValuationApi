using Microsoft.EntityFrameworkCore;
using ValuationApi.Models;

namespace ValuationApi.DB
{
    /// <summary>
    /// Db context. In memory db will be used for this application
    /// </summary>
	public class CustomDBContext : DbContext
    {
        public DbSet<Vessel> Vessels { get; set; }
        public DbSet<Valuation> Valuations { get; set; }
        public DbSet<Coefficient> Coefficients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("VesselDB");
        }
    }
}

