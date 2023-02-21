# Can also use .\SQLEXPRESS
$ConnectionString = "Data Source=WS124\SQLEXPRESS;
Initial Catalog=Northwind;
Integrated Security=true;
MultipleActiveResultSets=true;
TrustServerCertificate=true" # This line had to be added for Windows authentication.

dotnet ef dbcontext scaffold $ConnectionString Microsoft.EntityFrameworkCore.SqlServer,
--table Categories,
--table Products,
--output-dir AutoGenModels,
--namespace WorkingWithEFCore.AutoGen,
--data-annotations,
--context Northwind
# commas allow multi-line cmdlets