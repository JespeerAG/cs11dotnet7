using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // DbContext, DbContextOptionsBuilder
using Microsoft.EntityFrameworkCore.Diagnostics; // RelationalEventId


namespace Packt.Shared
{
    public class Northwind : DbContext
    {
        // For database table mappings
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Data Source=WS124\SQLEXPRESS;" +
            "Initial Catalog=Northwind;" +
            "Integrated Security=true;" +
            "MultipleActiveResultSets=true;" +
            "TrustServerCertificate=true;";

            ConsoleColor previousColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Green;
            // WriteLine($"Connection: {connection}"); // This runs for every query?
            ForegroundColor = previousColor;

            optionsBuilder.UseSqlServer(connection);

            optionsBuilder.LogTo(WriteLine // Logging to console - normally log to Debug or to a Trace Listener
            // LogTo takes an Action<string> delegate, which actions the string sent by optionsBuilder (in this case writing to Console)
            , new[] { RelationalEventId.CommandExecuting } // This filters the logged things
            ).EnableSensitiveDataLogging(); // Absolutely remove before deployment - only for debugging

            // LOADING TYPES
            // Without eager loading (given by .Include) QueryingCategories returns empty.

            // LAZY LOADING:
            // This is because QueryingCategories only pulls from Categories, not loading Products
            optionsBuilder.UseLazyLoadingProxies(); // Using Microsoft.EntityFrameworkCore.Proxies (package to NuGet)
            // This results in several calls to the database (one for the categories and one per category to get the products.)
            // The data is loaded JUST AS IT IS NEEDED, and not before.

            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Using Fluent API instead of attributes
            modelBuilder.Entity<Category>()
            .Property(category => category.CategoryName)
            .IsRequired() // NOT NULL
            .HasMaxLength(15);

            if (Database.ProviderName?.Contains("Sqlite") ?? false)
            {
                // Fixing the lack of decimal support in SQLite - unnecessary for my purposes, but won't hurt
                modelBuilder.Entity<Product>()
                .Property(product => product.Cost)
                .HasConversion<double>();

                
            }

            // The following filters at the source - virtually, this corresponds to a WHERE Discontinued == true on all queries.
            modelBuilder.Entity<Product>()
            .HasQueryFilter(p => !p.Discontinued);
        }
    }
}