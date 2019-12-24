using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokePredict.Database.Models
{
    public class Type
    {
        public string Name { get; set; }
        public List<DamageRelation> DamageTo { get; set; } = new List<DamageRelation>();
        public List<DamageRelation> DamageFrom { get; set; } = new List<DamageRelation>();
        public Type(PokeApiNet.Models.Type fromType)
        {
            var client = new PokeApiNet.PokeApiClient();
            Name = fromType.Name;
            var halfDamageTo = client.GetResourceAsync(fromType.DamageRelations.HalfDamageTo);
            var doubleDamageTo = client.GetResourceAsync(fromType.DamageRelations.DoubleDamageTo);
            var noDamageTo = client.GetResourceAsync(fromType.DamageRelations.NoDamageTo);
            var halfDamageFrom = client.GetResourceAsync(fromType.DamageRelations.HalfDamageFrom);
            var doubleDamageFrom = client.GetResourceAsync(fromType.DamageRelations.DoubleDamageFrom);
            var noDamageFrom = client.GetResourceAsync(fromType.DamageRelations.NoDamageFrom);

            halfDamageTo.Wait();
            halfDamageTo.Result.ForEach(type => DamageTo.Add(new DamageRelation
            {
                SourceType = fromType.Name,
                TargetType = type.Name,
                DamageMultiplier = .5f
            }));

            doubleDamageTo.Wait();
            doubleDamageTo.Result.ForEach(type => DamageTo.Add(new DamageRelation
            {
                SourceType = fromType.Name,
                TargetType = type.Name,
                DamageMultiplier = 2f
            }));

            noDamageTo.Wait();
            noDamageTo.Result.ForEach(type => DamageTo.Add(new DamageRelation
            {
                SourceType = fromType.Name,
                TargetType = type.Name,
                DamageMultiplier = 0f
            }));

            halfDamageFrom.Wait();
            halfDamageFrom.Result.ForEach(type => DamageFrom.Add(new DamageRelation
            {
                SourceType = fromType.Name,
                TargetType = type.Name,
                DamageMultiplier = .5f
            }));

            doubleDamageFrom.Wait();
            doubleDamageFrom.Result.ForEach(type => DamageFrom.Add(new DamageRelation
            {
                SourceType = fromType.Name,
                TargetType = type.Name,
                DamageMultiplier = 2f
            }));

            noDamageFrom.Wait();
            noDamageFrom.Result.ForEach(type => DamageFrom.Add(new DamageRelation
            {
                SourceType = fromType.Name,
                TargetType = type.Name,
                DamageMultiplier = 0f
            }));
        }
        public Type()
        {
        }
    }
    public class DamageRelation
    {
        [Key, Column(Order = 0)]
        public string SourceType { get; set; }
        [Key, Column(Order = 1)]
        public string TargetType { get; set; }
        [Key, Column(Order = 2)]
        public float DamageMultiplier { get; set; }
        public DamageRelation()
        {
        }
    }
}