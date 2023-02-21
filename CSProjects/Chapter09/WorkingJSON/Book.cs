using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

public class Book
{
    public Book(string title)
    {
        Title = title;
    }

    public string Title { get; set; }
    public string? Author { get; set; }

    [JsonInclude]
    public DateTime PublishDate;

    [JsonInclude]
    public DateTimeOffset Created;

    public ushort Pages;
}