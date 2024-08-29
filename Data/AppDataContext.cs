using Microsoft.EntityFrameworkCore;
using PokemonApi.Models;

namespace PokemonApi.Data
{
    public class AppDataContext : DbContext
    {
        public DbSet<Poke> Pokes { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
    }
}
