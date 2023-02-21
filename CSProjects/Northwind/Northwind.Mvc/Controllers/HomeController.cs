using System.Diagnostics; // Activity
using Microsoft.AspNetCore.Mvc; // Controller, IActionResult
using Northwind.Mvc.Models; // ErrorViewModel
using Packt.Shared; // NorthwindContext

namespace Northwind.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly NorthwindContext db;

    public HomeController(ILogger<HomeController> logger, NorthwindContext injectedContext)
    {
        _logger = logger;
        db = injectedContext;
    }

    public IActionResult Index()
    {
        _logger.LogError("This is a serious error (not really!)");
        _logger.LogWarning("First warning!");
        _logger.LogWarning("Second warning!");
        _logger.LogInformation("I am in the Index method of the HomeController.");

        HomeIndexViewModel model = new
        (
            VisitorCount: Random.Shared.Next(1, 1001),
            Categories: db.Categories.ToList(),
            Products: db.Products.ToList()
        );

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult ProductDetail(int? id) // By model binding, the id in the route is automatically passed in the method
    {
        if (!id.HasValue)
        {
            return BadRequest("You must pass a product ID for the route.");
        }

        Product? model = db.Products.SingleOrDefault(p => p.ProductId == id);

        if (model is null)
        {
            return NotFound($"ProductId {id} not found.");
        }

        return View(model);
    }

    public IActionResult ModelBinding()
    {
        return View();
    }

    [HttpPost] // This is added because both satisfy ModelBinding, and the system does not know which to use
    public IActionResult ModelBinding(Thing thing)
    {
      HomeModelBindingViewModel model = new(
        Thing: thing, HasErrors: !ModelState.IsValid,
        ValidationErrors: ModelState.Values
          .SelectMany(state => state.Errors)
          .Select(error => error.ErrorMessage)
      );
      return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
