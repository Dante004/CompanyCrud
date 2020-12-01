using CompanyCrud.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyCrud.Logic.Interfaces
{
    public interface ICompanyLogic
    {
        Task<Result<long>> AddCompany(Company company, CancellationToken token);
        Task<Result<long>> DeleteCompany(long id, CancellationToken token);
    }
}
