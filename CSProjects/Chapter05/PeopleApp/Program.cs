// See https://aka.ms/new-console-template for more information
using Packt.Shared;
using PacktLibraryModern;

Person sam = new()
{
    Name = "Sam",
    DateOfBirth = new(1969, 6, 25)
};

sam.FavoriteIceCream = "Chocolate Fudge";

// WriteLine($"Sam's favorite ice-cream flavor is {sam.FavoriteIceCream}.");

string color = "Red";
try
{
    sam.FavoritePrimaryColor = color;
    // WriteLine($"Sam's favorite primary color is {sam.FavoritePrimaryColor}.");
}
catch (Exception ex)
{
    WriteLine("Tried to set {0} to '{1}': {2}", nameof(sam.FavoritePrimaryColor), color, ex.Message);
}

// WriteLine(sam.Origin);
// WriteLine(sam.Greeting);
// WriteLine(sam.Age);
/*
Book book = new()
{
    Isbn = "978-1803237800",
    Title = "C# 11 and .NET 7 - Modern Cross-Platform Development Fundamentals"
};
*/
/*
Book book = new(isbn: "978-1803237800", title: "C# 11 and .NET 7 - Modern Cross-Platform Development Fundamentals")
{
    Author = "Mark J. Price",
    PageCount = 821
};

WriteLine("{0}: {1} written by {2} has {3:N0} pages.", book.Isbn, book.Title, book.Author, book.PageCount);
*/
/*
sam.Children.Add(new() { Name = "Charlie", DateOfBirth = new(2010, 3, 18) });
sam.Children.Add(new() { Name = "Ella", DateOfBirth = new(2020, 12, 24) });

WriteLine($"Sam's first child is {sam.Children[0].Name}.");
WriteLine($"Sam's second child is {sam.Children[1].Name}.");

WriteLine("With indexer:");
WriteLine($"Sam's first child is {sam[0].Name}.");
WriteLine($"Sam's second child is {sam[1].Name}.");

WriteLine($"Sam's child named Ella is {sam["Ella"].Age} years old.");
*/
/*
// Marriage bug: this supports one-sided polygamy!
Person al = new() { Name = "Sam" };
Person betty = new() { Name = "Betty" };
Person charlie = new() { Name = "Charlie" };

Person.Marry(al, betty);

charlie.Marry(al);

WriteLine($"Charlie's partner: {charlie.Spouse.Name}. Al's partner: {al.Spouse.Name}.");
*/

/*
Person lamech = new() { Name = "Lamech" };
Person adah = new() { Name = "Adah" };
Person zillah = new() { Name = "Zillah" };

lamech.Marry(adah);
// Person.Marry(zillah, lamech);
if (zillah + lamech)
{
    WriteLine($"{zillah.Name} and {lamech.Name} successfully got married.");
}

foreach (Person p in new List<Person> {lamech, adah, zillah}) {
    WriteLine($"{p.Name} is married to {p.Spouse?.Name ?? "nobody"}.");
}

Person baby1 = lamech.ProcreateWith(adah);
baby1.Name = "Jabal";
WriteLine($"{baby1.Name} was born on {baby1.DateOfBirth}");

Person baby2 = Person.Procreate(zillah, lamech);
baby2.Name = "Tubalcain";

Person baby3 = lamech * adah;
baby3.Name = "Jubal";

Person baby4 = zillah * lamech;
baby4.Name = "Naamah";

foreach (Person p in new List<Person> {lamech, adah, zillah}) {
    WriteLine($"{p.Name} has {p.Children.Count} children.");
}

for (int i = 0; i < lamech.Children.Count; i++)
{
    WriteLine("{0}'s child #{1} is named \"{2}\".", lamech.Name, i, lamech[i].Name);
}
*/

/*
int number = -1;

try
{
    WriteLine($"{number}! is {Person.Factorial(number)}");
}
catch (Exception ex)
{
    WriteLine($"{ex.GetType()} says: {ex.Message} number was {number}.");
}
*/

Passenger[] passengers = {
    new FirstClassPassenger { AirMiles = 1_419, Name = "Suman" },
    new FirstClassPassenger { AirMiles = 16_562, Name = "Lucy" },
    new BusinessClassPassenger { Name = "Janice" },
    new CoachClassPassenger { CarryOnKG = 25.7, Name = "Dave" },
    new CoachClassPassenger { CarryOnKG = 0, Name = "Amit" }
};

foreach (Passenger passenger in passengers)
{
    decimal flightCost = passenger switch
    {
        FirstClassPassenger p when p.AirMiles > 35000 => 1500M,
        FirstClassPassenger p when p.AirMiles > 15000 => 1750M,
        FirstClassPassenger _ => 2000M, // The _ is unnecessary as of C# 9
        BusinessClassPassenger _ => 1000M,
        CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
        CoachClassPassenger _ => 650M,
        _ => 800M
    };

    decimal flightCostCS9 = passenger switch
    {
        FirstClassPassenger p => p.AirMiles switch
        {
            > 35000 => 1500M,
            > 15000 => 1750M,
            _ => 2000M
        },
        /* Alternative:
        FirstClassPassenger { AirMiles: > 35000} => 1500M,
        FirstClassPassenger { AirMiles: > 15000} => 1750M,
        FirstClassPassenger => 2000M,
        */
        BusinessClassPassenger => 1000M,
        CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
        CoachClassPassenger => 650M,
        _ => 800M
    };
    WriteLine($"Flight costs {flightCost:C} for {passenger}. Done with C# 9, we get {flightCostCS9:C}.");
}