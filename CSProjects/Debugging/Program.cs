// See https://aka.ms/new-console-template for more information

Console.WriteLine("Test!");
int quantityIndex = 0;
int valueIndex = 1;

string[] record = new string[] { "10", "20" };

var a = (Decimal.Parse(record[quantityIndex]) * (1)).ToString("0.00"); // Units
var b = (Decimal.Parse(record[valueIndex]) * (-1)).ToString("0.00");

Console.WriteLine($"a is {a}, b is {b}");