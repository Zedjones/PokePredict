using System;
using System.IO;
using PokePredict.Database.Fetch;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {
            var basePath = Path.Combine("..", "PokemonFiles");
            PokemonFetch.CacheAll(basePath);
            Console.WriteLine("Hello World!");
        }
    }
}
