// See https://aka.ms/new-console-template for more information
string[] names = new[] { "Michael", "Pam", "Jim", "Dwight", "Angela", "Kevin", "Toby", "Creed" };

SectionTitle("Deferred execution");

// Linq extension method
var query1 = names.Where(name => name.EndsWith("m"));

// Query comprehension syntax
var query2 = from name in names where name.EndsWith("m") select name;

string[] result1 = query1.ToArray();

List<string> result2 = query2.ToList();

foreach (string name in query1)
{
    WriteLine(name);
    names[2] = "Jimmy"; // Due to deferred execution, query1 is executed again when checking the foreach
}

SectionTitle("Writing queries");

var query = names.Where(new Func<string, bool>(NameLongerThanFour));

// var query = names.Where(NameLongerThanFour); // This also works, and is shorter.

foreach (string item in query)
{
    WriteLine(item);
}

SectionTitle("Lambda expression");

/*
A lambda expression defines a function to be used on the spot.
The form of the function is given by names and .Where.
-> Names: this is an array of strings, so the argument of the lambda function is a string.
-> .Where: this requires for a boolean to be returned, so the lambda function returns a bool.
name => name.Length > 4 is a shortened way to say that given a (string) name input, this should be sent to the bool given by (name.Length > 4).
*/
var lambdaQuery = names.Where(name => name.Length > 4); // This is convenient because it allows to use methods only once, without having to define them.
var lambdaQueryExplicit = names.Where(NameLongerThanFourLambda); // This relies on a faster way to write explicit methods.

foreach (string item in lambdaQuery)
{
    WriteLine(item);
}

SectionTitle("Ordering");

var orderingQuery = names
    .Where(name => name.Length > 4)
    .OrderBy(name => name.Length);

// Notes: this uses a default comparer for the collections.
// The method implements stable sort, i.e. it preserves the existing order where the comparison is equal. Use unstable sorting when this is not required.
// For reverse ordering, use OrderByDescending.

var orderingQuery2 = names
    .Where(name => name.Length > 4)
    .OrderBy(name => name.Length)
    .ThenBy(name => name);
// This orders by length, and alphabetically at equal lengths

WriteLine("{0, 10} | {1, -10} | {2, -20}", "Position", "Length", "Length + Alphabetical");
WriteLine("------------------------------------------------");
for (int i = 0; i < orderingQuery.Count(); i++)
{
    WriteLine("{0, 10} | {1, -10} | {2, -10}", i, orderingQuery.ElementAt(i), orderingQuery2.ElementAt(i));
}

// Using the identity function for alphabetical ordering
var orderingAlphabetical = names
    .OrderBy(name => name);

// From .NET 7:
var orderingAlphabeticalQuick = names
    .Order();
var orderingReverseAlphabeticalQuick = names
    .OrderDescending();

SectionTitle("Filtering by type");

List<Exception> exceptions = new()
{
    new ArgumentException(),
    new SystemException(),
    new IndexOutOfRangeException(),
    new InvalidOperationException(),
    new NullReferenceException(),
    new InvalidCastException(),
    new OverflowException(),
    new DivideByZeroException(),
    new ApplicationException()
};

IEnumerable<ArithmeticException> arithmeticExceptionsQuery = exceptions
    .OfType<ArithmeticException>();

foreach (ArithmeticException exception in arithmeticExceptionsQuery)
{
    WriteLine(exception);
}

SectionTitle("Cohorts");

string[] cohort1 = new[]
{
    "Rachel", "Gareth", "Jonathan", "George"
};

string[] cohort2 = new[]
{
    "Jack", "Stephen", "Daniel", "Jack", "Jared"
};

string[] cohort3 = new[]
{
    "Declan", "Jack", "Jack", "Jasmine", "Conor"
};

string[][] cohorts = new string[][] {cohort1, cohort2, cohort3};

for (int i = 0; i < cohorts.Length; i++)
{
    Output(cohorts[i], $"Cohort {i + 1}");
}

SectionTitle("Set operations");

Output(cohort2.Distinct(), "cohort2.Distinct()");
Output(cohort2.DistinctBy(name => name.Substring(0, 2)), "cohort2.DistinctBy(name => name.Substring(0, 2))");
Output(cohort2.Union(cohort3), "cohort2.Union(cohort3)");
Output(cohort2.Concat(cohort3), "cohort2.Concat(cohort3)");
Output(cohort2.Intersect(cohort3), "cohort2.Intersect(cohort3)");
Output(cohort2.Except(cohort3), "cohort2.Except(cohort3)");
Output(cohort1.Zip(cohort2, (c1, c2) => $"{c1} matched with {c2}"), "cohort1.Zip(cohort2)");
