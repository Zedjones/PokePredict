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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DamageRelation>().HasKey(dr =>
                new { dr.SourceType, dr.TargetType, dr.DamageMultiplier }
            );
            builder.Entity<Move>().HasKey(move => new { move.Name });
            builder.Entity<Pokemon>().HasKey(mon => new { mon.Name });
            builder.Entity<Stat>().HasKey(stat => new { stat.Name, stat.PokeName });
            builder.Entity<StatChange>().HasKey(statChange => new { statChange.Name, statChange.MoveName });
            builder.Entity<Type>().HasKey(type => new { type.Name });
        }
    }
}