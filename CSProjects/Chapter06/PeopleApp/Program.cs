// See https://aka.ms/new-console-template for more information
using Packt.Shared;

Person harry = new()
{
    Name = "Harry",
    DateOfBirth = new(2001, 3, 25)
};

// harry.WriteToConsole();

// Non-generics: could cause issues
/*
System.Collections.Hashtable lookupObject = new();
lookupObject.Add(key: 1, value: "Alpha");
lookupObject.Add(key: 2, value: "Beta");
lookupObject.Add(key: 3, value: "Gamma");
lookupObject.Add(key: harry, value: "Delta");

int key = 2;

WriteLine($"Key {key} has value {lookupObject[key]}.");
WriteLine($"Key {harry} has value {lookupObject[key]}.");
*/

// Generics:
/*
Dictionary<int, string> lookupIntString = new();
lookupIntString.Add(key: 1, value: "Alpha");
lookupIntString.Add(key: 2, value: "Beta");
lookupIntString.Add(key: 3, value: "Gamma");
// This line errors because harry is not an int, nor is there a conversion to int for harry.
// lookupIntString.Add(key: harry, value: "Delta");
lookupIntString.Add(key: 4, value: "Delta");

int key = 3;

WriteLine($"Key {key} has value {lookupIntString[key]}.");
*/

// This is only allowed for delegates
// harry.Shout = Harry_Shout;

/*
harry.Shout += Harry_Shout;
harry.Shout += Harry_Shout2;

harry.Poke();
harry.Poke();
harry.Poke();
harry.Poke();
*/
/*
Person?[] people =
{
    null,
    new() {Name = "Simon"},
    new() {Name = "Jenny"},
    new() {Name = "Adam"},
    new() {Name = null},
    new() {Name = "Richard"},
};

OutputPeopleNames(people, "Initial list of people:");

Array.Sort(people, new PersonComparer());

OutputPeopleNames(people, "After sorting using Person's IComparer implementation");
*/

/*
DisplacementVector dv1 = new(3, 5);
DisplacementVector dv2 = new(-2, 7);
DisplacementVector dv3 = dv1 + dv2;
DisplacementVector dv4 = new();
WriteLine($"({dv4.X}, {dv4.Y})");

WriteLine($"({dv1.X}, {dv1.Y}) + ({dv2.X}, {dv2.Y}) = ({dv3.X}, {dv3.Y})");
*/

/*

Employee john = new()

{
    Name = "John Jones",
    DateOfBirth = new(year: 1990, month: 7, day: 28)
};

john.EmployeeCode = "JJ001";
john.HireDate = new(year: 2014, month: 11, day: 23);
WriteLine($"{john.Name} was hired on {john.HireDate:dd-MM-yy}.");

john.WriteToConsole();

WriteLine(john); // WriteLine automatically casts john as an object, and uses the ToString() method. If ToString is not overridden, the object implementation is used.
// WriteLine((object)john.ToString())
*/
/*
Employee aliceInEmployee = new() { Name = "Alice", EmployeeCode = "AA123" };
Person aliceInPerson = aliceInEmployee;
// aliceInEmployee.WriteToConsole();
// aliceInPerson.WriteToConsole();
// WriteLine(aliceInEmployee.ToString());
// WriteLine(aliceInPerson.ToString());

// Employee explicitAlice = (Employee)aliceInPerson; // Vulnerable to exceptions

if (aliceInPerson is Employee)
{
    WriteLine($"{nameof(aliceInPerson)} IS an Employee.");

    Employee explicitAlice = (Employee)aliceInPerson;
}
*/

/*
Employee john = new()

{
    Name = "John Jones",
    DateOfBirth = new(year: 1990, month: 7, day: 28)
};

try
{
    john.TimeTravel(when: new(1999, 12, 31));
    john.TimeTravel(when: new(1950, 12, 25));
}
catch (PersonException ex)
{
    WriteLine(ex.Message);
}
*/
List<string> emails = new() { "pamela@test.com", "ian&test.com" };

foreach (string email in emails)
{
    WriteLine("{0} is a valid e-mail address: {1}", email, email.IsValidEmail());
    // WriteLine("{0} is a valid e-mail address: {1}", email, StringExtensions.IsValidEmail(email)); // This still works!
}