# Mini Banking Management System using .NET 8

## notes

### To Solve dot net ef not found issue
> dotnet tool install --global dotnet-ef --version 7

### .NET CLI
> dotnet ef dbcontext scaffold "Server=.;Database=testDb;User ID=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext
