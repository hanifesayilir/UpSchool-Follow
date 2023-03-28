using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext

    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Country> Accounts { get; set; }
        public DbSet<City> Accounts { get; set; }

    }
}
