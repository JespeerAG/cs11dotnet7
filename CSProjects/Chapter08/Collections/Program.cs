// See https://aka.ms/new-console-template for more information
using System.Collections.Immutable;

string input = args[0];

if (input.Equals("list"))
{
    List<string> cities = new();
    cities.Add("London");
    cities.Add("Paris");
    cities.Add("Milan");

    cities.AddRange(new[] { "Rome", "Vienna" });

    Output("Initial list", cities);
    WriteLine($"The first city is {cities[0]}");
    WriteLine($"The first city is {cities[cities.Count - 1]}");

    cities.Insert(0, "Sydney");

    Output("After inserting Sydney at index 0", cities);

    cities.RemoveAt(1);
    cities.Remove("Milan");
    Output("After removing two cities", cities);

    ImmutableList<string> immutableCities = cities.ToImmutableList();
    ImmutableList<string> newList = immutableCities.Add("Rio");

    Output("Immutable list of cities:", immutableCities);
    Output("New immutable list of cities:", newList);
}

if (input.Equals("dictionary"))
{
    Dictionary<string, string> keywords = new();

    keywords.Add(key: "int", value: "32-bit integer data type");

    keywords.Add("long", "64-bit integer data type");
    keywords.Add("float", "Single precision floating point number");

    /*
    Dictionary<string, string> keywords = new()
    {
        ["int"] = "32-bit integer data type",
        ["long"] = "64-bit integer data type",
        ["float"] = "Single precision floating point number", // This comma is optional
    }
    */

    Output("Dictionary keys:", keywords.Keys);
    Output("Dictionary values:", keywords.Values);

    WriteLine("Keywords and their definitions");
    foreach(KeyValuePair<string, string> item in keywords)
    {
        WriteLine($"    {item.Key}: {item.Value}");
    }

    string key = "long";
    WriteLine($"The definition of {key} is {keywords[key]}");
}

if (input.Equals("queue"))
{
    Queue<string> coffee = new();

    coffee.Enqueue("Damir");
    coffee.Enqueue("Andrea");
    coffee.Enqueue("Ronald");
    coffee.Enqueue("Amin");
    coffee.Enqueue("Irina");

    Output("Initial queue from front to back", coffee);

    string served = coffee.Dequeue();
    WriteLine($"Served: {served}");

    served = coffee.Dequeue();
    WriteLine($"Served: {served}");
    Output("Current queue from front to back", coffee);

    WriteLine($"{coffee.Peek()} is next in line.");
    Output("Current queue from front to back", coffee);
}

if (input.Equals("priorityQueue"))
{
    PriorityQueue<string, int> vaccine = new();

    // 1 = High Priority
    // 2 = Medium Priority
    // 3 = Low Priority

    vaccine.Enqueue("Pamela", 1);
    vaccine.Enqueue("Rebecca", 3);
    vaccine.Enqueue("Juliet", 2);
    vaccine.Enqueue("Ian", 1);

    OutputPQ("Current queue for vaccination:", vaccine.UnorderedItems);

    WriteLine($"{vaccine.Dequeue()} has been vaccinated.");
    WriteLine($"{vaccine.Dequeue()} has been vaccinated.");
    OutputPQ("Current queue for vaccination:", vaccine.UnorderedItems);
    
    WriteLine($"{vaccine.Dequeue()} has been vaccinated.");

    WriteLine("Adding Mark to queue with priority 2");
    vaccine.Enqueue("Mark", 2);

    WriteLine($"{vaccine.Peek()} will be next to be vaccinated.");
    OutputPQ("Current queue for vaccination:", vaccine.UnorderedItems);
}

if (input.Equals("range"))
{
    string name = "Samantha Jones";

    int lengthOfFirst = name.IndexOf(' ');
    int lengthOfLast = name.Length - lengthOfFirst - 1;

    string firstName = name.Substring(startIndex: 0, length: lengthOfFirst);
    string lastName = name.Substring(startIndex: name.Length - lengthOfLast, length: lengthOfLast);

    WriteLine($"First name: {firstName}. Last name: {lastName}.");

    // Using spans:

    ReadOnlySpan<char> nameAsSpan = name.AsSpan();
    ReadOnlySpan<char> firstNameSpan = nameAsSpan[0..lengthOfFirst];
    ReadOnlySpan<char> lastNameSpan = nameAsSpan[^lengthOfLast..^0];

    WriteLine($"First name: {firstNameSpan}. Last name: {lastNameSpan}.");
}
