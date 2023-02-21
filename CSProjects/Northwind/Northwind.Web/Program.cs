using Packt.Shared; // AddNorthwindContext extension method
using Microsoft.AspNetCore.Server.Kestrel.Core; // HttpProtocols

// configure services

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddNorthwindContext();
builder.Services.AddRequestDecompression();

// Configuring HTTP/3. Not supported by 25% of browsers.
builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.ListenAnyIP(5001, ListenOptions =>
    {
        ListenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
        ListenOptions.UseHttps(); // HTTP/3 requires secure connections
    });
});

var app = builder.Build();

// configure the HTTP pipeline

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseRequestDecompression();
    // app.UseDeveloperExceptionPage();
}

app.Use(async (HttpContext context, Func<Task> next) =>
{
    RouteEndpoint? rep = context.GetEndpoint() as RouteEndpoint;

    if (rep is not null)
    {
        WriteLine($"Endpoint name: {rep.DisplayName}");
        WriteLine($"Endpoint route pattern: {rep.RoutePattern.RawText}");
    }

    if (context.Request.Path == "/bonjour")
    {
        // in the case of a match on URL path, this becomes a terminating delegate that returns, so does not call the next delegate
        await context.Response.WriteAsync("Bonjour Monde!");
        return;
    }

    // we could modify the request before calling the next delegate

    await next();

    // we could modify the response after calling the next delegate
});

app.UseHttpsRedirection();

app.UseDefaultFiles(); // automatically identifies index.html, default.html, ...
app.UseStaticFiles(); // looks in wwwroot for static files

app.MapRazorPages(); // maps URL paths such as /suppliers to the Razor Page named suppliers.cshtml in /Pages
app.MapGet("/hello", () => "Hello World!"); // maps URL paths such as /hello to an inline delegate.

// start the web server

app.Run();

WriteLine("This executes after the web server has stopped!");
