using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Packt.Shared
{
    public partial class Person : System.Object
    {
        public string? Name;
        public DateTime DateOfBirth;
        public readonly string HomePlanet = "Earth";
        public readonly DateTime Instantiated;
        public WondersOfTheAncientWorld FavoriteAncientWonder;
        public WondersOfTheAncientWorld BucketList;

        public List<Person> Children = new();

        public Person()
        {
            Name = "Unknown";
            Instantiated = DateTime.Now;
        }
        public Person(string initialName, string homePlanet)
        {
            Name = initialName;
            HomePlanet = homePlanet;
            Instantiated = DateTime.Now;
        }
    }
}