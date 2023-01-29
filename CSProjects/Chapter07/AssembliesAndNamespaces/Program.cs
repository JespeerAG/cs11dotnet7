// See https://aka.ms/new-console-template for more information
using System.Xml.Linq; // XDocument

XDocument doc = new();

// Difference between string and String
string s1 = "Hello";
String s2 = "World"; // This is implicitly requiring an import of System

WriteLine($"{s1} {s2}");