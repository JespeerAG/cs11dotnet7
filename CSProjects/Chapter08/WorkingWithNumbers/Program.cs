// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Text.RegularExpressions;

// string input = Console.ReadLine()!;

string input = args[0];

if (input.Equals("large"))
{
    WriteLine("Working with large integers:");
    WriteLine("----------------------------------------");

    ulong big = ulong.MaxValue;
    WriteLine($"{big,40:N0}");

    BigInteger bigger = BigInteger.Parse("123456789012345678901234567890");
    WriteLine($"{bigger,40:N0}");
}

if (input.Equals("complex"))
{
    WriteLine("Working with complex numbers:");

    Complex c1 = new(real: 4, imaginary: 2);
    Complex c2 = new(real: 3, imaginary: 7);
    Complex c3 = c1 + c2;

    WriteLine($"{c1} added to {c2} is {c3}");

    string ComplexString(Complex c)
    {
        return $"{c.Real} + {c.Imaginary}i";
    }

    WriteLine($"{ComplexString(c1)} + {ComplexString(c2)} = {ComplexString(c3)}");
}

if (input.Equals("text"))
{
    string city = "London";
    WriteLine($"{city} is {city.Length} characters long.");

    WriteLine($"First char is {city[0]} and fourth is {city[3]}.");

    string cities = "Paris,Tehran,Chennai,Sydney,New York,Medellin";

    string[] citiesArray = cities.Split(',');

    WriteLine($"There are {citiesArray.Length} cities in the array.");

    foreach(string item in citiesArray)
    {
        WriteLine(item);
    }
}

if (input.Equals("textManipulation"))
{
    string[] fullNames = new string[3] { "Alan Shore", "Peter Parker", "Clark Kent" };

    (string, string) NameSplitter(string fullNameInput)
    {
        int indexOfTheSpace = fullNameInput.IndexOf(' ');
        string firstName = fullNameInput.Substring(startIndex: 0, length: indexOfTheSpace);
        string lastName = fullNameInput.Substring(startIndex: indexOfTheSpace + 1);

        return (firstName, lastName);
    }


    for (int i = 0; i < fullNames.Length; i++)
    {
        string fullName = fullNames[i];
        (string firstName, string lastName) = NameSplitter(fullName);
        if (i != 0) WriteLine("-------------------------------");
        WriteLine($"Complete name {i + 1} is {fullName}:");
        WriteLine($"First name: {firstName}");
        WriteLine($"Last name: {lastName}");
    }
}

if(input.Equals("regex"))
{
    Write("Enter your age: ");
    string inputAge = ReadLine()!;

    // Regex ageChecker = new(digitsOnlyText);
    Regex ageChecker = DigitsOnly();

    if (ageChecker.IsMatch(inputAge))
    {
        WriteLine("Thank you!");
    }
    else
    {
        WriteLine($"This is not a valid age: {inputAge}");
    }
}

if(input.Equals("csvRegex"))
{
    string films = """
    "Monsters, Inc.","I, Tonya","Lock, Stock, and Two Smoking Barrels"
    """;

    // Regex csv = new(commaSeparatorText);
    Regex csv = CommaSeparator();

    MatchCollection filmsSmart = csv.Matches(films);

    WriteLine("Splitting with regular expressions:");
    foreach (Match film in filmsSmart)
    {
        WriteLine(film.Groups[2].Value);
    }
}