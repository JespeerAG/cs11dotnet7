using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared; // NorthwindContext, Suppliers

namespace PacktFeatures.Pages;

public class CountriesPageModel : PageModel
{
    private NorthwindContext db;

    public List<SuppliersByCountry> Countries { get; set; } = new();

    public CountriesPageModel(NorthwindContext injectedContext)
    {
        db = injectedContext;

        var query = db.Suppliers
            .GroupBy(s => s.Country);

        foreach (var result in query)
        {
            if (result.Key is not null) Countries.Add(new SuppliersByCountry(result.Key, result));
        }
    }


    public void OnGet()
    {
        ViewData["Title"] = "Northwind B2B - Countries";
    }
}
