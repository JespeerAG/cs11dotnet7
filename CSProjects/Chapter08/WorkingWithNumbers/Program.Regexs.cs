using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

partial class Program
{
    [GeneratedRegex(digitsOnlyText, RegexOptions.IgnoreCase)]
    public static partial Regex DigitsOnly();

    [GeneratedRegex(commaSeparatorText, RegexOptions.IgnoreCase)]
    public static partial Regex CommaSeparator();
}