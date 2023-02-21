// See https://aka.ms/new-console-template for more information
using Packt.Shared;
using Microsoft.EntityFrameworkCore; // Include

string? input = args[0];

string? queryInput = args[1];

bool printQuery = queryInput switch
{
    "Y" => true,
    _ => false
};


// Northwind db = new();
//WriteLine($"Provider: {db.Database.ProviderName}");
// QueryingCategories();
// FilteredIncludes();

if (input.ToUpper().Equals("QUERYINGCATEGORIES")) QueryingCategories(printQuery);

if (input.ToUpper().Equals("QUERYINGCATEGORIES2")) QueryingCategories2(printQuery);

if (input.ToUpper().Equals("FILTEREDINCLUDES")) FilteredIncludes(printQuery);

if (input.ToUpper().Equals("QUERYINGPRODUCTS")) QueryingProducts(printQuery);

if (input.ToUpper().Equals("QUERYINGWITHLIKE")) QueryingWithLike(printQuery);

if (input.ToUpper().Equals("GETRANDOMPRODUCT")) GetRandomProduct(printQuery);

if (input.ToUpper().Equals("ADDPRODUCT"))
{
    
    var resultAdd = AddProduct(categoryId: 6, productName: "Bob's Burgers", price: 520M);
    var resultAdd2 = AddProduct(categoryId: 6, productName: "Bob's Burgers", price: 500M);
    var resultAdd3 = AddProduct(categoryId: 6, productName: "Bob's Burgers", price: 480M);

    if (resultAdd.affected == 1)
    {
        WriteLine($"Add product successful with ID: {resultAdd.productId}.");
    }

    if (resultAdd2.affected == 1)
    {
        WriteLine($"Add product successful with ID: {resultAdd2.productId}.");
    }

    if (resultAdd3.affected == 1)
    {
        WriteLine($"Add product successful with ID: {resultAdd3.productId}.");
    }

    ListProducts(productIdsToHighlight: new int[] {resultAdd.productId, resultAdd2.productId, resultAdd3.productId});
}

if (input.ToUpper().Equals("DELETEPRODUCT"))
{
    WriteLine("About to delete all products whose name starts with Bob.");
    Write("Press ENTER to continue or any other key to exit: ");

    if (ReadKey(intercept: true).Key == ConsoleKey.Enter)
    {
        int deleted = DeleteProducts(productNameStartsWith: "Bob");
        if (deleted == 1) WriteLine($"{deleted} product was deleted.");
        else WriteLine($"{deleted} products were deleted.");
    }

    else
    {
        WriteLine("Delete was cancelled.");
    }
}

if (input.ToUpper().Equals("UPDATEBETTER"))
{
    var resultUpdateBetter = IncreaseProductPricesBetter(productNameStartsWith: "Bob", amount: 20M);

    if (resultUpdateBetter.affected > 0)
    {
        WriteLine("Increase product price successful.");
    }

    ListProducts(productIdsToHighlight: resultUpdateBetter.productIds);
}

if (input.ToUpper().Equals("DELETEBETTER"))
{
    WriteLine("About to delete all products whose name starts with Bob, but BETTAH.");
    Write("Press ENTER to continue or any other key to exit: ");

    if (ReadKey(intercept: true).Key == ConsoleKey.Enter)
    {
        int deleted = DeleteProductsBetter(productNameStartsWith: "Bob");
        if (deleted == 1) WriteLine($"{deleted} product was deleted.");
        else WriteLine($"{deleted} products were deleted.");
    }

    else
    {
        WriteLine("Delete was cancelled.");
    }
}

if (input.ToUpper().Equals("SERIALIZE"))
{
    WriteLine("Creating four files containing serialized categories and products.");

using (Northwind db = new())
{
    // a query to get all categories and their related products 
    IQueryable<Category>? categories = db.Categories?.Include(c => c.Products);

    if (categories is null)
    {
    WriteLine("No categories found.");
    return;
    }

    GenerateXmlFile(categories);
    GenerateXmlFile(categories, useAttributes: false);
    GenerateCsvFile(categories);
    GenerateJsonFile(categories);

    WriteLine($"Current directory: {Environment.CurrentDirectory}");
    }
}