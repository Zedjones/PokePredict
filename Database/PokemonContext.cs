using Microsoft.EntityFrameworkCore;
using PokePredict.Database.Models;

namespace PokePredict.Database
{
    public class PokemonContext : DbContext
    {
        public DbSet<Pokemon> Pokemon { get; set; }
    }
}