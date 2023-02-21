using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Packt.Shared; // Northwind, Category, Product
using Microsoft.EntityFrameworkCore; // DbSet<T>
using System.Xml.Linq;

partial class Program
{
    static void FilterAndSort()
    {
        SectionTitle("Filter and sort");

        using (Northwind db = new())
        {
            DbSet<Product> allProducts = db.Products;

            IQueryable<Product> processedProducts = allProducts.ProcessSequence(); // This step does nothing, aside from showing how to implement extension methods.

            IQueryable<Product> filteredProducts = processedProducts.Where(product => product.UnitPrice < 10M);

            IOrderedQueryable<Product> sortedAndFilteredProducts = filteredProducts.OrderByDescending(product => product.UnitPrice);

            var projectedProducts = sortedAndFilteredProducts
                .Select(product => new
                {
                    product.ProductId,
                    product.ProductName,
                    product.UnitPrice
                }); // This uses anonymous types to chop down on the columns used

            WriteLine("Products that cost less than $10:");

            foreach (var p in projectedProducts)
            {
                WriteLine($"{p.ProductId}: {p.ProductName} costs {p.UnitPrice:$#,##0.00}");
            }
            WriteLine();
            Highlighter(projectedProducts.ToQueryString());
        }
    }

    static void JoinCategoriesAndProducts()
    {
        SectionTitle("Join categories and products");

        using (Northwind db = new())
        {
            var queryJoin = db.Categories.Join( // Start from db.Categories
                inner: db.Products, // Join db.Products
                outerKeySelector: category => category.CategoryId, // The left side of the join associates to each category in db.Categories the object category.CategoryId
                innerKeySelector: product => product.CategoryId, // The right side of the join associates to each product in db.Products the object product.CategoryId
                resultSelector: (c, p) => new
                {
                    c.CategoryName,
                    p.ProductName,
                    p.ProductId
                } // Given a pair (c, p), where c is a Category and p is a Product, our query will only consider c.CategoryName, p.ProductName and p.ProductId (this is a projection)
            ).OrderBy(cp => cp.CategoryName); // This orders any item cp = { c.CategoryName, p.ProductName, p.ProductId } by cp.CategoryName, so by the categories

            foreach (var item in queryJoin)
            {
                WriteLine($"{item.ProductId}: {item.ProductName} is in {item.CategoryName}.");
            }
        }
    }

    static void GroupJoinCategoriesAndProducts()
    {
        SectionTitle("Group join categories and products");

        using (Northwind db = new())
        {
            var queryGroup = db.Categories.AsEnumerable().GroupJoin( // The AsEnumerable is required to do a conversion from expression trees to SQL. We are using LINQ to EFCore to bring data, and LINQ to Objects to treat it. Not always efficient.
                inner: db.Products,
                outerKeySelector: category => category.CategoryId,
                innerKeySelector: product => product.CategoryId,
                resultSelector: (c, matchingProducts) => new
                {
                    c.CategoryName,
                    Products = matchingProducts.OrderBy(p => p.ProductName)
                }
            );

            /* DIFFERENCES BETWEEN GroupJoin AND Join:
            1. The Join works like an inner join. The GroupJoin works like an outer join.
            2. The Join has a row per match, exactly like SQL. The GroupJoin has a row for each left-table entry, and will collate a group of matched entries on the right (matchingProducts)
            */

            foreach (var category in queryGroup)
            {
                WriteLine($"{category.CategoryName} has {category.Products.Count()} products.");

                foreach (var product in category.Products)
                {
                    WriteLine($" -  {product.ProductName}");
                }
            }
        }
    }

    static void AggregateProducts()
    {
        SectionTitle("Aggregate products");
        string format = "{0, -25} {1, 10}";

        using (Northwind db = new())
        {
            if (db.Products.TryGetNonEnumeratedCount(out int countDbSet))
            {
                WriteLine(format, "Product count from DbSet:", countDbSet);
            }
            else
            {
                WriteLine("Products DbSet does not have a Count property.");
            }

            List<Product> products = db.Products.ToList();

            if (products.TryGetNonEnumeratedCount(out int countList))
            {
                WriteLine(format, "Product count from list: ", countList);
            }
            else
            {
                WriteLine("Products list does not have a Count property.");
            }

            WriteLine(format, "Product count:", db.Products.Count());

            WriteLine("{0, -27} {1, 8}", "Discontinued product count:", db.Products.Count(products => products.Discontinued));

            WriteLine("{0, -25} {1, 10:$#,##0.00}", "Highest product price:", db.Products.Max(p => p.UnitPrice));
            WriteLine("{0, -25} {1, 10:N0}", "Sum of units in stock:", db.Products.Sum(p => p.UnitsInStock));
            WriteLine("{0, -25} {1, 10:N0}", "Sum of units on order:", db.Products.Sum(p => p.UnitsOnOrder));
            WriteLine("{0, -25} {1, 10:$#,##0.00}", "Average unit price", db.Products.Average(p => p.UnitPrice));
            WriteLine("{0, -25} {1, 10:$#,##0.00}", "Value of units in stock:", db.Products.Sum(p => p.UnitPrice * p.UnitsInStock));
        }
    }

    static async Task TrickyQuestion()
    {
        SectionTitle("Tricky behaviour from Count");
        IEnumerable<Task> tasks = Enumerable.Range(start: 0, count: 2)
            .Select(_ => Task.Run(() => Write("+")));

        await Task.WhenAll(tasks);

        // tasks.Count goes over each individual task in tasks, making them run again!
        Write($"{tasks.Count()} stars!");
    }

    static void OutputTableOfProducts(Product[] products, int currentPage, int totalPages)
    {
        string line = new('-', count: 73);
        string lineHalf = new('-', count: 30);
        string format = "{0, 4} {1, -40} {2, 12} {3, -15}";
        string formatNumerical = "{0, 4} {1, -40} {2, 12:C} {3, -15}";


        WriteLine(line);
        WriteLine(format, "ID", "Product Name", "Unit Price", "Discontinued");
        WriteLine(line);

        foreach (Product p in products)
        {
            WriteLine(formatNumerical, p.ProductId, p.ProductName, p.UnitPrice, p.Discontinued);
        }

        WriteLine("{0} Page {1} of {2} {3}", lineHalf, currentPage + 1, totalPages + 1, lineHalf);
    }

    static void OutputPageOfProducts(IQueryable<Product> products, int pageSize, int currentPage, int totalPages)
    {
        var pagingQuery = products.OrderBy(p => p.ProductId)
            .Skip(currentPage * pageSize).Take(pageSize);

        // SectionTitle(pagingQuery.ToQueryString()); // To see the OFFSET ... ROWS FETCH NEXT ... ROWS ONLY query (note how the ordering is necessary)

        OutputTableOfProducts(pagingQuery.ToArray(), currentPage, totalPages);
    }

    static void PagingProducts()
    {
        SectionTitle("Paging products");

        using (Northwind db = new())
        {
            int pageSize = 10;
            int currentPage = 0;
            int productCount = db.Products.Count();
            int totalPages = productCount / pageSize;

            while(true)
            {
                OutputPageOfProducts(db.Products, pageSize, currentPage, totalPages);

                Write("Press <- to page back, press -> to page forward, any key to exit.");
                ConsoleKey key = ReadKey().Key;

                if (key == ConsoleKey.LeftArrow)
                {
                    if (currentPage == 0)
                    {
                        currentPage = totalPages;
                    }
                    else
                    {
                        currentPage--;
                    }
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    if (currentPage == totalPages)
                    {
                        currentPage = 0;
                    }
                    else
                    {
                        currentPage++;
                    }
                }
                else
                {
                    break;
                }

                WriteLine();
            }
        }
    }
    static void CustomExtensionMethods()
    {
        SectionTitle("Custom aggregate extension methods");

        using (Northwind db = new())
        {
            WriteLine("{0, -25} {1, 10:N0}", "Mean units in stock:", db.Products.Average(p => p.UnitsInStock));
            WriteLine("{0, -25} {1, 10:$#,##0.00}", "Mean unit price:", db.Products.Average(p => p.UnitPrice));
            WriteLine("{0, -25} {1, 10:N0}", "Median units in stock:", db.Products.Median(p => p.UnitsInStock));
            WriteLine("{0, -25} {1, 10:$#,##0.00}", "Median unit price:", db.Products.Median(p => p.UnitsInStock));
            WriteLine("{0, -25} {1, 10:N0}", "Mode units in stock:", db.Products.Mode(p => p.UnitsInStock));
            WriteLine("{0, -25} {1, 10:$#,##0.00}", "Mode unit price:", db.Products.Mode(p => p.UnitsInStock));
        }
    }

    static void OutputProductsAsXml()
    {
        SectionTitle("Output products as XML");

        using (Northwind db = new())
        {
            Product[] productsArray = db.Products.ToArray();

            XElement xml = new
            (
                "products",
                from p in productsArray
                select new XElement
                (
                    "product",
                    new XAttribute("id", p.ProductId),
                    p.UnitPrice is null ? null : new XAttribute("price", p.UnitPrice),
                    new XElement("name", p.ProductName)
                )
            );

            WriteLine(xml.ToString());
        }
    }

    static void ProcessSettings()
    {
        string path = Path.Combine(Environment.CurrentDirectory, "settings.xml");

        WriteLine($"Settings file path: {path}");
        XDocument doc = XDocument.Load(path);
        var appSettings = doc.Descendants("appSettings")
            .Descendants("add")
            .Select(node => new
            {
                Key = node.Attribute("key")?.Value,
                Value = node.Attribute("value")?.Value
            }).ToArray();

        foreach (var item in appSettings)
        {
            WriteLine($"{item.Key}: {item.Value}");
        }
    }
}