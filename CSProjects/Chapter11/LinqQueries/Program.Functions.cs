using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;

partial class Program
{
    public static void FullCities()
    {
        string citiesList;
        using (Northwind db = new())
        {
            IQueryable<string?> cities = db.Customers.Select(c => c.City).Distinct();
            citiesList = String.Join(", ", cities.ToArray());
        }
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.Blue;
        WriteLine("** CITIES **");
        WriteLine(citiesList);
        ForegroundColor = previousColor;
    }


    public static void PrintCustomers(IQueryable<Customer?>? customers)
    {
        if (customers is null) return;
        foreach(Customer? customer in customers)
        {
            if (customer is not null)
                WriteLine($" - {customer.CustomerId}: {customer.CompanyName}");
        }
    }
    public static void CustomersInCityProgram()
    {
        FullCities();
        using (Northwind db = new())
        {
            Write("Enter the name of a city from the above: ");
            string input = ReadLine()!;
            IQueryable<Customer?>? customersInCity = db.Customers?.Where(c => c.City == null ? false : (c.City).Equals(input));
            WriteLine("There are {0} customers in {1}:", customersInCity == null ? 0 : customersInCity.Count(), input);
            PrintCustomers(customersInCity);
        }
    }
}