using System;
using System.IO;
using PokeFetch;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {
            var basePath = Path.Combine("..", "PokemonFiles");
            PokemonFetch.CacheAll(basePath);
        }
    }
}
