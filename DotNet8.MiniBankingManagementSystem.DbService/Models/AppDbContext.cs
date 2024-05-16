using Microsoft.EntityFrameworkCore;

namespace DotNet8.MiniBankingManagementSystem.DbService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tbl_Account> Tbl_Account { get; set; }
    }
}
