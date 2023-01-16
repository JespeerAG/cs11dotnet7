// See https://aka.ms/new-console-template for more information
using CallStackExceptionHandlingLib;
using static System.Console;

Console.WriteLine("In Main");
Alpha();

void Alpha()
{
    Console.WriteLine("In Alpha");
    Beta();
}

void Beta()
{
    Console.WriteLine("In Beta");
    try
    {
        Calculator.Gamma();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Caught this: {ex.Message}");
        // throw ex; // Throws in Beta
        throw; // Throws in the original caller
    }
}