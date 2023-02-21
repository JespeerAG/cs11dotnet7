// See https://aka.ms/new-console-template for more information
using System.Text.Json;

using static System.Environment;
using static System.IO.Path;

Book myBook = new(title: "C# 11 and .NET 7 - Modern Cross-Platform Development Fundamentals")
{
    Author = "Mark J Price",
    PublishDate = new (2022, 11, 8),
    Pages = 823,
    Created = DateTimeOffset.UtcNow
};

JsonSerializerOptions options = new()
{
    IncludeFields = true,
    PropertyNameCaseInsensitive = true,
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

string filePath = Combine(CurrentDirectory, "myBook.json");

using (Stream fileStream = File.Create(filePath))
{
    JsonSerializer.Serialize<Book>(utf8Json: fileStream, value: myBook, options);
}

WriteLine($"Written {new FileInfo(filePath).Length} bytes of JSON to {filePath}.");

WriteLine();

WriteLine(File.ReadAllText(filePath));