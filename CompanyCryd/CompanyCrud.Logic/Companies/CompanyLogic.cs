using CompanyCrud.Logic.Interfaces;
using CompanyCrud.Models;
using CompanyCrud.Models.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyCrud.Logic.Companies
{
    public class CompanyLogic : ICompanyLogic
    {
        private readonly DataContext _dataContext;

        public CompanyLogic(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result<long>> AddCompany(Company company, CancellationToken token)
        {
            if(company == null)
            {
                return Result.Error<long>("Company cannot be null");
            }

            await _dataContext.Companies.AddAsync(company, token);
            await _dataContext.SaveChangesAsync();

            return Result.Ok(company.ID);
        }

        public async Task<Result<long>> DeleteCompany(long id, CancellationToken token)
        {
            var company = await _dataContext
                .Companies
                .Include(x => x.Employes)
                .FirstOrDefaultAsync(x => x.ID == id);

            if(company == null)
            {
                return Result.Error<long>("Company with that id doesn't exist");
            }

            _dataContext.Employees.RemoveRange(company.Employes);
            _dataContext.Companies.Remove(company);
            await _dataContext.SaveChangesAsync();

            return Result.Ok(id);
        }
    }
}
