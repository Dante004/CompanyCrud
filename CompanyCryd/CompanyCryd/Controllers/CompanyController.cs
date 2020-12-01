using CompanyCrud.Dto;
using CompanyCrud.Logic;
using CompanyCrud.Logic.Interfaces;
using CompanyCrud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyLogic _logic;

        public CompanyController(ICompanyLogic logic)
        {
            _logic = logic;
        }

        [HttpPost("/company/create")]
        public async Task<IActionResult> Create(Company company, CancellationToken token = default)
        {
            var result = await _logic.AddCompany(company, token);

            if(!result.Success)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(Create), result.Value);
        }

        [HttpPost("company/search")]
        public async Task<IActionResult> Search(SearchDto searchDto)
        {
            var result = await _logic.Search(searchDto.Keyword,
                searchDto.EmployeeDateOfBirthFrom,
                searchDto.EmployeeDateOfBirthTo,
                searchDto.EmployeeJobTitles);

            if (!result.Success)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(ModelState);
            }

            return Ok(result.Value);
        }

        [HttpPut("company/update/{id}")]
        public async Task<IActionResult> Update(long id,[FromBody] Company company, CancellationToken token = default)
        {
            var result = await _logic.GetCompany(id, token);

            if (!result.Success)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(ModelState);
            }

            //mapowanie

            return NoContent();
        }

        [HttpDelete("company/delete/{id}")]
        public async Task<IActionResult> Delete(long id, CancellationToken token = default)
        {
            var result = await _logic.DeleteCompany(id, token);

            if (!result.Success)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(ModelState);
            }

            return Accepted();
        }
    }
}
