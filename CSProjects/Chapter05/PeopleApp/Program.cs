// See https://aka.ms/new-console-template for more information
using Packt.Shared;

Person bob = new();
string format = "{0} was born on {1:dddd, d MMMM yyyy}";

bob.Name = "Bob Smith";
bob.DateOfBirth = new DateTime(1965, 12, 22);

WriteLine(format, bob.Name, bob.DateOfBirth);

Person alice = new()
{
    Name = "Alice Jones",
    DateOfBirth = new(1998, 3, 7)
};

WriteLine(format, bob.Name, bob.DateOfBirth);
