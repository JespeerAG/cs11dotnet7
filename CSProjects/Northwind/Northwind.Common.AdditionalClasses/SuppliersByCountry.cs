namespace Packt.Shared;
public class SuppliersByCountry
{
    public string? Country;
    public IEnumerable<Supplier>? Suppliers = null!;

    public SuppliersByCountry()
    {
        
    }

    public SuppliersByCountry(string country, IEnumerable<Supplier>? suppliers)
    {
        this.Country = country;
        this.Suppliers = suppliers;
    }
}
