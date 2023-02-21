using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

partial class Program
{
    static bool NameLongerThanFour (string name)
    {
        return name.Length > 4;
    }

    // This is the same as the above, in lambda notation.
    static bool NameLongerThanFourLambda(string name) => name.Length > 4;

    static bool LambdaShorthandIf(string name) => name.Length > 4 ? true : false; // condition ? true_expression : false_expression, Excel-like.
}