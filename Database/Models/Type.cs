using System.Collections.Generic;
using System.Linq;

namespace PokePredict.Database.Models
{
    public class Type
    {
        public string Name { get; set; }
        public TypeRelations DamageRelations { get; set; }
        public Type(PokeApiNet.Models.Type fromType)
        {
            Name = fromType.Name;
            DamageRelations = new TypeRelations(fromType.DamageRelations);
        }
        public Type()
        {
        }
    }
    public class TypeRelations
    {

        public List<string> HalfDamageTo { get; set; } = new List<string>();
        public List<string> DoubleDamageTo { get; set; } = new List<string>();
        public List<string> NoDamageTo { get; set; } = new List<string>();
        public List<string> HalfDamageFrom { get; set; } = new List<string>();
        public List<string> DoubleDamageFrom { get; set; } = new List<string>();
        public List<string> NoDamageFrom { get; set; } = new List<string>();
        public TypeRelations(PokeApiNet.Models.TypeRelations fromRelations)
        {
            var client = new PokeApiNet.PokeApiClient();
            var halfDamageTo = client.GetResourceAsync(fromRelations.HalfDamageTo);
            var doubleDamageTo = client.GetResourceAsync(fromRelations.DoubleDamageTo);
            var noDamageTo = client.GetResourceAsync(fromRelations.NoDamageTo);
            var halfDamageFrom = client.GetResourceAsync(fromRelations.HalfDamageFrom);
            var doubleDamageFrom = client.GetResourceAsync(fromRelations.DoubleDamageFrom);
            var noDamageFrom = client.GetResourceAsync(fromRelations.NoDamageFrom);

            halfDamageTo.Wait();
            halfDamageTo.Result.ForEach(type => HalfDamageTo.Add(type.Name));

            doubleDamageTo.Wait();
            doubleDamageTo.Result.ForEach(type => DoubleDamageTo.Add(type.Name));

            noDamageTo.Wait();
            noDamageTo.Result.ForEach(type => NoDamageTo.Add(type.Name));

            halfDamageFrom.Wait();
            halfDamageFrom.Result.ForEach(type => HalfDamageFrom.Add(type.Name));

            doubleDamageFrom.Wait();
            doubleDamageFrom.Result.ForEach(type => DoubleDamageFrom.Add(type.Name));

            noDamageFrom.Wait();
            noDamageFrom.Result.ForEach(type => NoDamageFrom.Add(type.Name));
        }
        public TypeRelations()
        {
        }
    }
}