// See https://aka.ms/new-console-template for more information
using Packt.Shared;
using Microsoft.EntityFrameworkCore;

/*
using (Northwind db = new())
{
    
    // DbSet<Customer> customersBasic = db.Customers;

    IQueryable<Customer>? customers = db.Customers;

    if ((customers is null) || (!customers.Any()))
    {
        WriteLine("No customers found!");
        return;
    }

    foreach (Customer c in customers)
    {
        WriteLine($"{c.CustomerId} : {c.CompanyName} (location: {c.City})");
    }
    
    IQueryable<Product>? products = db.Products.Where(p => 1 == 1);

    foreach (Product p in products)
    {
        WriteLine($"{p.ProductId} : {p.ProductName} (location: {p.UnitPrice})");
    }
}
*/
CustomersInCityProgram();