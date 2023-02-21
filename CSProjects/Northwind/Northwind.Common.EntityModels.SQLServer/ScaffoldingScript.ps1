# Can also use .\SQLEXPRESS
<# $ConnectionString = "Data Source=WS124\SQLEXPRESS;
Initial Catalog=Northwind;
Integrated Security=true;
MultipleActiveResultSets=true;
TrustServerCertificate=true" #> # This line had to be added for Windows authentication.

$ConnectionString = @(Get-Content -Path ..\DatabaseSetup\ConnectionString.txt -Delimiter '#')
# Read the Help article on -Delimiter (The newline breaks the content)

# Need to read up how to do this stuff (C:\Users\Tobia.Beccari\AppData\Roaming\Microsoft\UserSecrets)
#dotnet user-secrets init
# dotnet user-secrets set ConnectionStrings:Northwind $ConnectionString

dotnet ef dbcontext scaffold $ConnectionString Microsoft.EntityFrameworkCore.SqlServer,
# --output-dir AutoGenModels,
--namespace Packt.Shared,
--data-annotations,
--context Northwind
# commas allow multi-line cmdlets