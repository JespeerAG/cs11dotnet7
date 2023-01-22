// See https://aka.ms/new-console-template for more information
/*
int thisCannotBeNull = 4;

// This gives an error
// thisCannotBeNull = null;

WriteLine(thisCannotBeNull);

int? thisCouldBeNull = null;
Nullable<int> thisCouldAlsoBeNull = null;

WriteLine(thisCouldBeNull);
WriteLine(thisCouldBeNull.GetValueOrDefault());

thisCouldBeNull = 7;

WriteLine(thisCouldBeNull);
WriteLine(thisCouldBeNull.GetValueOrDefault());
*/

using NullHandling;

/*
Address address = new()
{
    Building = null,
    Street = null!,
    City = "London",
    Region = "UK"
};

WriteLine(address.Building?.Length);
WriteLine(address.Street.Length);
*/

string? authorName = null;

// NullReferenceException:
// int x = authorName.Length;

int? y = authorName?.Length;

WriteLine(y);

int result = authorName?.Length ?? 3;
WriteLine(result);