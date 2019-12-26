using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

namespace PokePredict.Database.Models
{
    public class Type
    {
        public string Name { get; set; }
        public List<DamageRelation> DamageTo { get; set; } = new List<DamageRelation>();
        public List<DamageRelation> DamageFrom { get; set; } = new List<DamageRelation>();
        private string WritePath;
        public Type(PokeApiNet.Models.Type fromType, string precedingPath, int depth = 1)
        {
            WritePath = Path.Combine(precedingPath, "Types", fromType.Name + ".json");
            Name = fromType.Name;
            if (depth <= 0) {
                return;
            }
            var client = new PokeApiNet.PokeApiClient();
            var halfDamageTo = client.GetResourceAsync(fromType.DamageRelations.HalfDamageTo);
            var doubleDamageTo = client.GetResourceAsync(fromType.DamageRelations.DoubleDamageTo);
            var noDamageTo = client.GetResourceAsync(fromType.DamageRelations.NoDamageTo);
            var halfDamageFrom = client.GetResourceAsync(fromType.DamageRelations.HalfDamageFrom);
            var doubleDamageFrom = client.GetResourceAsync(fromType.DamageRelations.DoubleDamageFrom);
            var noDamageFrom = client.GetResourceAsync(fromType.DamageRelations.NoDamageFrom);

            halfDamageTo.Wait();
            halfDamageTo.Result.ForEach(type => DamageTo.Add(new DamageRelation
            (
                type.Name, .5f, depth, precedingPath, type
            )));

            doubleDamageTo.Wait();
            doubleDamageTo.Result.ForEach(type => DamageTo.Add(new DamageRelation
            (
                type.Name, 2f, depth, precedingPath, type
            )));

            noDamageTo.Wait();
            noDamageTo.Result.ForEach(type => DamageTo.Add(new DamageRelation
            (
                type.Name, 0f, depth, precedingPath, type
            )));

            halfDamageFrom.Wait();
            halfDamageFrom.Result.ForEach(type => DamageFrom.Add(new DamageRelation
            (
                type.Name, .5f, depth, precedingPath, type
            )));

            doubleDamageFrom.Wait();
            doubleDamageFrom.Result.ForEach(type => DamageFrom.Add(new DamageRelation
            (
                type.Name, 2f, depth, precedingPath, type
            )));

            noDamageFrom.Wait();
            noDamageFrom.Result.ForEach(type => DamageFrom.Add(new DamageRelation
            (
                type.Name, 0f, depth, precedingPath, type
            )));
        }
        public Type()
        {
        }
        public void WriteOut()
        {
            var dirName = Path.GetDirectoryName(WritePath);
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            var typeStr = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(WritePath, typeStr);
        }
    }
    public class DamageRelation
    {
        public Type TargetType { get; set; }
        public string TargetTypeName { get; set; }
        public float DamageMultiplier { get; set; }
        public DamageRelation()
        {
        }

        public DamageRelation(string targetTypeName, float damageMultiplier, int depth, string precedingPath,
                              PokeApiNet.Models.Type targetType = null)
        {
            TargetType = new Type(targetType, precedingPath, depth - 1);
            TargetTypeName = targetTypeName;
            DamageMultiplier = damageMultiplier;
        }
    }
}