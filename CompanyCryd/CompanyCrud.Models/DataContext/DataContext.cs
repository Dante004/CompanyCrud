using Microsoft.EntityFrameworkCore;

namespace CompanyCrud.Models.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) :
            base(options)
        {
            Database.Migrate();
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
