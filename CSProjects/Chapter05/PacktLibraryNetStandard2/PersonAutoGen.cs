using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packt.Shared
{
    public partial class Person
    {
        public string? FavoriteIceCream { get; set; }

        private string? favoritePrimaryColor;

        public string? FavoritePrimaryColor
        {
            get
            {
                return favoritePrimaryColor;
            }
            set
            {
                switch (value?.ToLower())
                {
                    case "red":
                    case "green":
                    case "blue":
                        favoritePrimaryColor = value;
                        break;
                    default:
                        throw new ArgumentException(
                            $"{value} is not a primary color. " +
                            "Choose from: red, green, blue."
                        );
                }
            }
        }
        public string Origin
        {
            get
            {
                return string.Format("{0} was born on {1}", Name, HomePlanet);
            }
        }
        public string Greeting => $"{Name} says 'Hello!'";
        public int Age => DateTime.Today.Year - DateOfBirth.Year;
    }
}