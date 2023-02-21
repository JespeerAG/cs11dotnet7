using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Packt.Shared; // Northwind, Category, Product
using Microsoft.EntityFrameworkCore; // Include extension method
using Microsoft.EntityFrameworkCore.ChangeTracking; // CollectionEntry


partial class Program
{
    static void QueryingCategories(bool includeQuery = false)
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

            if (includeQuery)
            {
                Info($"ToQueryString: {categories.ToQueryString()}");
            }
        }
    }   

    static void FilteredIncludes(bool includeQuery = false)
    {
        using (Northwind db = new())
        {
            SectionTitle("Products with a minimum number of units in stock.");

            string? input;
            int stock;

            do
            {
                Write("Enter a minimum for units in stock: ");
                input = ReadLine();
            } while (!int.TryParse(input, out stock));

            IQueryable<Category>? categories = db.Categories?.Include(c => c.Products.Where(p => p.Stock >= stock));

            if ((categories is null) || (!categories.Any()))
            {
                Fail("No categories found.");
                return;
            }

            foreach (Category c in categories)
            {
                WriteLine($"{c.CategoryName} has {c.Products.Count} products with a minimum of {stock} units in stock.");
                foreach (Product p in c.Products)
                {
                    WriteLine($"   {p.ProductName} has {p.Stock} units in stock.");
                }
            }

            if (includeQuery)
            {
                Info($"ToQueryString: {categories.ToQueryString()}");
            }
        }
    }

    static void QueryingProducts(bool includeQuery = false)
    {
        using (Northwind db = new())
        {
            SectionTitle("Products that cost more than a price, highest at top.");

            string? input;
            decimal price;

            do
            {
                Write("Enter a product price: ");
                input = ReadLine();
            } while (!decimal.TryParse(input, out price));

            IQueryable<Product>? products = db.Products?
            .TagWith("Products filtered by price and sorted.") // This is useful for identifying log messages.
            .Where(product => product.Cost > price)
            .OrderByDescending(product => product.Cost);

            if ((products is null) || (!products.Any())) // .Any() is more efficient than .Count() == 0
            {
                Fail("No products found");
                return;
            }

            foreach (Product p in products)
            {
                WriteLine("{0}: {1} costs {2:$#,##0.00} and has {3} in stock.", p.ProductId, p.ProductName, p.Cost, p.Stock);
            }

            if (includeQuery)
            {
                Info($"ToQueryString: {products.ToQueryString()}");
            }
        }
    }

    static void QueryingWithLike(bool includeQuery = false)
    {
        using (Northwind db = new())
        {
            SectionTitle("Pattern matching with LIKE.");

            Write("Enter part of a product name: ");
            string? input = ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Fail("You betrayed me. You did not enter part of a product name.");
                return;
            }

            IQueryable<Product>? products = db.Products?
            .Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));

            if ((products is null) || (!products.Any()))
            {
                Fail("No products found.");
                return;
            }

            foreach (Product p in products)
            {
                WriteLine($"{p.ProductName} has {p.Stock} units in stock. Discontinued? {p.Discontinued}");
            }

            if (includeQuery)
            {
                Info($"ToQueryString: {products.ToQueryString()}");
            }
        }
    }

    static void GetRandomProduct(bool includeQuery = false)
    {
        using (Northwind db = new())
        {
            SectionTitle("Get a random product.");

            int? rowCount = db.Products?.Count();

            if (rowCount == null)
            {
                Fail("Products table is empty");
                return;
            }

            Product? p = db.Products?.FirstOrDefault(
                p => p.ProductId == (int)(EF.Functions.Random() * rowCount)
            );

            if (p is null)
            {
                Fail("Product not found.");
                return;
            }

            WriteLine($"Random product: {p.ProductId} {p.ProductName}");

            if (includeQuery)
            {
                Info($"For methods like FirstOrDefault, Count(), ... we need EF Core query logging. This is not tracked if not in the log (Id 20100). Sorry!");
            }
        }
    }
}