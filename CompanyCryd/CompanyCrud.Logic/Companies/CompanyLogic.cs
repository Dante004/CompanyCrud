using CompanyCrud.Logic.Interfaces;
using CompanyCrud.Models;
using CompanyCrud.Models.DataContext;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyCrud.Logic.Companies
{
    public class CompanyLogic : ICompanyLogic
    {
        private readonly DataContext _dataContext;
        private readonly IValidator<Company> _validator;

        public CompanyLogic(DataContext dataContext,
            IValidator<Company> validator)
        {
            _dataContext = dataContext;
            _validator = validator;
        }

        public async Task<Result<long>> AddCompany(Company company, CancellationToken token)
        {
            if(company == null)
            {
                return Result.Error<long>("Company cannot be null");
            }

            var validatorResult = await _validator.ValidateAsync(company);

            if(!validatorResult.IsValid)
            {
                return Result.Error<long>(validatorResult.Errors);
            }

            await _dataContext.Companies.AddAsync(company, token);
            await _dataContext.SaveChangesAsync(token);

            return Result.Ok(company.ID);
        }

        public async Task<Result<long>> DeleteCompany(long id, CancellationToken token)
        {
            var company = await _dataContext
                .Companies
                .Include(x => x.Employes)
                .FirstOrDefaultAsync(x => x.ID == id, token);

            if(company == null)
            {
                return Result.Error<long>("Company with that id doesn't exist");
            }

            _dataContext.Employees.RemoveRange(company.Employes);
            _dataContext.Companies.Remove(company);
            await _dataContext.SaveChangesAsync(token);

            return Result.Ok(id);
        }

        public async Task<Result<Company>> GetCompany(long id, CancellationToken token)
        {
            var company = await _dataContext
                .Companies
                .Include(x => x.Employes)
                .FirstOrDefaultAsync(x => x.ID == id, token);

            if (company == null)
            {
                return Result.Error<Company>("Company with that id doesn't exist");
            }

            return Result.Ok(company);
        }

        public async Task<Result<List<Company>>> Search(string keyword, DateTime DateOfBirthFrom, DateTime DateOfBirthTo, JobTitle jobTitle, CancellationToken token)
        {
            IQueryable<Company> query = _dataContext.Companies;
            if (DateOfBirthFrom != default && DateOfBirthTo != default)
                query = query.Include(x => x.Employes
                           .Where(x => x.DateOfBirth >= DateOfBirthFrom && x.DateOfBirth <= DateOfBirthTo && x.JobTitle == jobTitle));
            else
                query = query.Include(x => x.Employes);

            query = query.WhereIf(!string.IsNullOrEmpty(keyword), x => x.Name.Contains(keyword));

            return Result.Ok(await query.ToListAsync(token));
        }

        public async Task<Result> UpdateCompany(Company company, CancellationToken token)
        {
            if(company == null)
            {
                return Result.Error<Company>("Company cannot be null");
            }

            var validatorResult = await _validator.ValidateAsync(company);

            if (!validatorResult.IsValid)
            {
                return Result.Error<Company>(validatorResult.Errors);
            }

            await _dataContext.SaveChangesAsync(token);
            return Result.Ok();
        }
    }
}
