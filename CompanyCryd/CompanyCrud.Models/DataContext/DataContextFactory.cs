using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CompanyCrud.Models.DataContext
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDb;Database=CompanyCrud;Trusted_Connection=True;");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
