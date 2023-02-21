
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Packt.Shared; // Northwind, Category, Product
using Microsoft.EntityFrameworkCore; // Include extension method
using Microsoft.EntityFrameworkCore.ChangeTracking; // CollectionEntry


partial class Program
{

    // This is for understanding that once things are loaded, they stay loaded within Northwind db.
    // Whenever we use a new Northwind db, we reload (bad for speed, good for up-to-date)
    // If we use eager loading the first time around, we don't need to do any loading afterwards - the first loading was sufficient.
    static void QueryingCategories2(bool includeQuery = false)
    {
        using (Northwind db = new())
        {
            SectionTitle("Categories and how many products they have:");

            IQueryable<Category>? categories;

            db.ChangeTracker.LazyLoadingEnabled = false;

            Write("Enable eager loading? (Y/N): ");
            bool eagerLoading = (ReadKey(intercept: true).Key == ConsoleKey.Y);
            bool explicitLoading = false;
            WriteLine();

            // EagerLoading
            if (eagerLoading)
            {
                categories = db.Categories?.Include(c => c.Products);
            }
            else
            {
                categories = db.Categories;
                Write("Enable explicit loading? (Y/N): ");
                explicitLoading = (ReadKey(intercept: true).Key == ConsoleKey.Y);
                WriteLine();
            }

            if ((categories is null) || (!categories.Any()))
            {
                Fail("No categories found.");
                return;
            }

            foreach (Category c in categories)
            {
                if (explicitLoading)
                {
                    Write($"Explicitly load products for {c.CategoryName}? (Y/N): ");
                    ConsoleKeyInfo key = ReadKey(intercept: true);
                    WriteLine();

                    if (key.Key == ConsoleKey.Y)
                    {
                        CollectionEntry<Category, Product> products = db.Entry(c).Collection(c2 => c2.Products);

                        if (!products.IsLoaded) products.Load();
                    }
                }

                WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
            }




            IQueryable<Category>? categories2;

            db.ChangeTracker.LazyLoadingEnabled = false;

            Write("Enable eager loading? (Y/N): ");
            bool eagerLoading2 = (ReadKey(intercept: true).Key == ConsoleKey.Y);
            bool explicitLoading2 = false;
            WriteLine();

            // EagerLoading
            if (eagerLoading2)
            {
                categories2 = db.Categories?.Include(c => c.Products);
            }
            else
            {
                categories2 = db.Categories;
                Write("Enable explicit loading? (Y/N): ");
                explicitLoading2 = (ReadKey(intercept: true).Key == ConsoleKey.Y);
                WriteLine();
            }

            if ((categories2 is null) || (!categories2.Any()))
            {
                Fail("No categories found.");
                return;
            }

            foreach (Category c in categories2)
            {
                if (explicitLoading2)
                {
                    Write($"Explicitly load products for {c.CategoryName}? (Y/N): ");
                    ConsoleKeyInfo key = ReadKey(intercept: true);
                    WriteLine();

                    if (key.Key == ConsoleKey.Y)
                    {
                        CollectionEntry<Category, Product> products2 = db.Entry(c).Collection(c2 => c2.Products);

                        if (!products2.IsLoaded) products2.Load();
                    }
                }

                WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
            }



            if (includeQuery)
            {
                Info($"ToQueryString: {categories.ToQueryString()}");
            }
        }
    }   
}