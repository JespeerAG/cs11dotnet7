// See https://aka.ms/new-console-template for more information
// Regex ageChecker = new(digitsOnlyText);
using System.Text.RegularExpressions;

string regex = @"^[a-z]+$"; // Only text

while (true)
{
    WriteLine($"The default regex is {regex}");
    Write("Enter a regular expression or press ENTER to use the default: ");
    string? input = ReadLine();

    if (!string.IsNullOrWhiteSpace(input))
    {
        regex = input;
    }

    Regex checker = new(regex);

    WriteLine("Enter some input: ");
    string checkedInput = ReadLine()!;

    WriteLine($"{checkedInput} matches {regex}? {checker.IsMatch(checkedInput)}");

    WriteLine("Press ESC to end or any key to try again.");

    ConsoleKey cki = ReadKey().Key;
    if (cki == ConsoleKey.Escape)
    {
        break;
    }
}
