using Microsoft.EntityFrameworkCore;
using PokePredict.Database.Models;

namespace PokePredict.Database
{
    public class PokemonContext : DbContext
    {
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<Move> Moves { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=pokemon.db");
        }
    }
}