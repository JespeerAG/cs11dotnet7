using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inheritance
{
    public abstract class Shape
    {
        public virtual double Height { get; set; }
        public virtual double Width { get; set; }
        public virtual double Area { get; }

        public abstract string Print();
    }

    public class Rectangle : Shape
    {
        public Rectangle(double height, double width)
        {
            Height = height;
            Width = width;
        }
        public override double Area
        {
            get
            {
                return Height * Width;
            }
        }

        public override string Print()
        {
            return "Rectanguloid";
        }
    }

    public class Square : Rectangle
    {
        public Square (double side) : base(side, side)
        {

        }
        
        public override string Print()
        {
            return "Squork";
        }
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public override double Height
        {
            get
            {
                return 2 * Radius;
            }
        }
        public override double Width
        {
            get
            {
                return 2 * Radius;
            }
        }
        public override double Area
        {
            get
            {
                return Math.PI * Radius * Radius;
            }
        }

        public override string Print()
        {
            return "Kirkle";
        }
    }
}