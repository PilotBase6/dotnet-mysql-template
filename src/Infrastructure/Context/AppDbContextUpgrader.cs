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
                        "server=db;database=asterisk",
                        Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.19-mariadb"));
            }
        }
    }
}   