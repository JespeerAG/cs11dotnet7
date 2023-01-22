using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Packt.Shared
{
    public static class StringExtensions
    {
        public static bool IsValidEmail(this string input)
        {
            string regexMatch = @"[a-zA-Z0-9\.-_]+@[a-zA-Z0-9\.-_]+";
            return Regex.IsMatch(input, regexMatch);
        }
    }
}