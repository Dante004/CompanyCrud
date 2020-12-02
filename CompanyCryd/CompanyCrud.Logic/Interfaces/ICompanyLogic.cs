using CompanyCrud.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyCrud.Logic.Interfaces
{
    public interface ICompanyLogic
    {
        Task<Result<long>> AddCompany(Company company, CancellationToken token);
        Task<Result<long>> DeleteCompany(long id, CancellationToken token);
        Task<Result<Company>> GetCompany(long id, CancellationToken token);
        Task<Result> UpdateCompany(Company company, CancellationToken token);
        Task<Result<List<Company>>> Search(string keyword, DateTime From, DateTime To, JobTitle jobTitle, CancellationToken token);
    }
}
