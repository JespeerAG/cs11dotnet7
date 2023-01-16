// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "log.txt");

Console.WriteLine($"Writing to: {logPath}");

TextWriterTraceListener logFile = new(File.CreateText(logPath));

Trace.Listeners.Add(logFile);
Trace.AutoFlush = true;

Debug.WriteLine("Debug: Watching.");
Trace.WriteLine("Trace: Watching.");

Console.WriteLine("Reading from appsettings.json in {0}", Directory.GetCurrentDirectory());

ConfigurationBuilder builder = new();

builder.SetBasePath(Directory.GetCurrentDirectory());

builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

IConfigurationRoot configuration = builder.Build();

TraceSwitch ts = new(
    displayName: "PacktSwitch",
    description: "This switch is set via a JSON config."
);

configuration.GetSection("PacktSwitch").Bind(ts);

Trace.WriteLineIf(ts.TraceError, "Trace Error");
Trace.WriteLineIf(ts.TraceWarning, "Trace Warning");
Trace.WriteLineIf(ts.TraceInfo, "Trace Information");
Trace.WriteLineIf(ts.TraceVerbose, "Trace Verbose");

int unitsInStock = 12;
LogSourceDetails(unitsInStock > 10);

Console.ReadLine();