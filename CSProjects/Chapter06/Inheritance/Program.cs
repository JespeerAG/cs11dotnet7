// See https://aka.ms/new-console-template for more information
using Inheritance;

List<Shape> myShapes = new();

Rectangle r = new(height: 3, width: 4.5);
Square s = new(side: 5);
Circle c = new(radius: 2.5);

myShapes.Add(r);
myShapes.Add(s);
myShapes.Add(c);

foreach (Shape shape in myShapes)
{
    Console.WriteLine($"{shape.GetType().Name} (a.k.a. {shape.Print()}) H: {shape.Height}, W: {shape.Width}, A: {shape.Area}");
}
