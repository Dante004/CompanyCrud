using AutoMapper;
using CompanyCrud.Dto;
using CompanyCrud.Logic;
using CompanyCrud.Logic.Interfaces;
using CompanyCrud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyLogic _logic;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyLogic logic,
            IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        [HttpPost("/company/create")]
        public async Task<IActionResult> Create(CompanyDto companyDto, CancellationToken token = default)
        {
            var company = _mapper.Map<Company>(companyDto);

            var result = await _logic.AddCompany(company, token);

            if(!result.Success)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var createResult = new CreateResult
            {
                Id = result.Value
            };

            return CreatedAtAction(nameof(Create), createResult);
        }

        [HttpPost("company/search")]
        public async Task<IActionResult> Search(SearchDto searchDto, CancellationToken token = default)
        {
            var result = await _logic.Search(searchDto.Keyword,
                searchDto.EmployeeDateOfBirthFrom,
                searchDto.EmployeeDateOfBirthTo,
                searchDto.EmployeeJobTitles,
                token);

            if (!result.Success)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var searchResult = new SearchResult
            {
                Results = _mapper.Map<List<CompanyDto>>(result.Value)
            };

            return Ok(searchResult);
        }

        [HttpPut("company/update/{id}")]
        public async Task<IActionResult> Update(long id,[FromBody] CompanyDto companyDto, CancellationToken token = default)
        {
            var result = await _logic.GetCompany(id, token);

            if (!result.Success)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var updatedComapny = _mapper.Map(companyDto, result.Value);

            var updatedResult = await _logic.UpdateCompany(updatedComapny, token);

            if (!updatedResult.Success)
            {
                updatedResult.AddErrorToModelState(ModelState);
                return BadRequest(ModelState);
            }

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
