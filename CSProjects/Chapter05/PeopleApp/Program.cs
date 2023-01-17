// See https://aka.ms/new-console-template for more information
using Packt.Shared;

Person sam = new()
{
    Name = "Sam",
    DateOfBirth = new(1969, 6, 25)
};

sam.FavoriteIceCream = "Chocolate Fudge";

WriteLine($"Sam's favorite ice-cream flavor is {sam.FavoriteIceCream}.");

string color = "Red";
try
{
    sam.FavoritePrimaryColor = color;
    WriteLine($"Sam's favorite primary color is {sam.FavoritePrimaryColor}.");
}
catch (Exception ex)
{
    WriteLine("Tried to set {0} to '{1}': {2}", nameof(sam.FavoritePrimaryColor), color, ex.Message);
}

WriteLine(sam.Origin);
WriteLine(sam.Greeting);
WriteLine(sam.Age);
