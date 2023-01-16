using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PacktLibraryNetStandard2;

namespace Packt.Shared
{
    public class Person : System.Object
    {
        public string? Name;
        public DateTime DateOfBirth;
        public WondersOfTheAncientWorld FavoriteAncientWonder;
        public WondersOfTheAncientWorld BucketList;

        public List<Person> Children = new();
    }
}