using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // ExecuteUpdate, ExecuteDelete
using Microsoft.EntityFrameworkCore.ChangeTracking; // EntityEntry<T>
using Microsoft.EntityFrameworkCore.Storage; // IDbContextTransaction
using Packt.Shared; // Northwind, Product


partial class Program
{
    static void ListProducts(int[]? productIdsToHighlight = null)
    {
        using (Northwind db = new())
        {
            if ((db.Products is null) || (!db.Products.Any()))
            {
                Fail("There are no products.");
                return;
            }

            WriteLine("| {0, -3} | {1, -35} | {2, 8} | {3, 5} | {4} |", "Id", "Product Name", "Cost", "Stock", "Disc");

            foreach (Product p in db.Products)
            {
                ConsoleColor previousColor = ForegroundColor;

                if ((productIdsToHighlight is not null) && productIdsToHighlight.Contains(p.ProductId))
                {
                    ForegroundColor = ConsoleColor.Green;
                }

                WriteLine("| {0:000} | {1, -35} | {2, 8:$#,##0.00} | {3, 5} | {4} |", p.ProductId, p.ProductName, p.Cost, p.Stock, p.Discontinued);

                ForegroundColor = previousColor;
            }
        }
    }

    static (int affected, int productId) AddProduct(int categoryId, string productName, decimal? price)
    {
        using (Northwind db = new())
        {
            if (db.Products is null) return (0, 0);

            Product p = new()
            {
                CategoryId = categoryId,
                ProductName = productName,
                Cost = price,
                Stock = 72
            };

            EntityEntry<Product> entity = db.Products.Add(p);
            WriteLine($"State: {entity.State}, ProductId: {p.ProductId}");

            int affected = db.SaveChanges();
            WriteLine($"State: {entity.State}, ProductId: {p.ProductId}");

            return (affected, p.ProductId);
        }
    }

    static int DeleteProducts(string productNameStartsWith)
    {
        using (Northwind db = new())
        {
            using (IDbContextTransaction t = db.Database.BeginTransaction())
            {
                WriteLine($"Transaction isolation level: {t.GetDbTransaction().IsolationLevel}");

                IQueryable<Product>? products = db.Products?.Where(
                    p => p.ProductName.StartsWith(productNameStartsWith)
                );

                if ((products is null) || (!products.Any()))
                {
                    WriteLine("No products found to delete.");
                    return 0;
                }

                if (db.Products is null) return 0;
                
                db.Products.RemoveRange(products);

                int affected = db.SaveChanges();
                t.Commit();
                return affected;
            }
            
            
        }
    }

    static (int affected, int[]? productIds) IncreaseProductPricesBetter(string productNameStartsWith, decimal amount)
    {
        using (Northwind db = new())
        {
            if (db.Products is null) return (0, null);

            IQueryable<Product>? products = db.Products.Where(p => p.ProductName.StartsWith(productNameStartsWith));

            int affected = products.ExecuteUpdate(s => s.SetProperty(p => p.Cost, p => p.Cost + amount)); // First argument is property selected, second a rgument is the value to update it to

            int[] productIds = products.Select(p => p.ProductId).ToArray();

            return (affected, productIds);
        }
    }

    static int DeleteProductsBetter (string productNameStartsWith)
    {
        using (Northwind db = new())
        {
            int affected = 0;

            IQueryable<Product>? products = db.Products?.Where(
                p => p.ProductName.StartsWith(productNameStartsWith)
            );

            if ((products is null) || (!products.Any()))
            {
                WriteLine("No products found to delete.");
                return 0;
            }

            affected = products.ExecuteDelete();
            return affected;
        }
    }
}