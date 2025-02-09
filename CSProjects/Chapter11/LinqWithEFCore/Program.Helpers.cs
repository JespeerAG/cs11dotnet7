using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

partial class Program
{
    static void SectionTitle(string title)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine("*");
        WriteLine($" {title}");
        WriteLine("*");
        ForegroundColor = previousColor;
    }

    static void Highlighter(string highlightedString, ConsoleColor highlighterColor = ConsoleColor.Green)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = highlighterColor;
        WriteLine(highlightedString);
        ForegroundColor = previousColor;
    }
}    