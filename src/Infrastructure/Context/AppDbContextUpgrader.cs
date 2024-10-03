using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context{
    public partial class AppDbContextUpgrader : AppDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseMySql(
                        "Server=dotnet-test-mysql;Port=3306;Database=testdb;User Id=testdotnet;Password=testdotnet;Allow User Variables=true;Default Command Timeout=0;",
                        Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect("Server=dotnet-test-mysql;Port=3306;Database=testdb;User Id=testdotnet;Password=testdotnet;Allow User Variables=true;Default Command Timeout=0;"));
            }
        }
    }
}   