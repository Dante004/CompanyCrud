using AutoMapper;
using CompanyCrud.Dto;
using CompanyCrud.Models;

namespace CompanyCrud.Mappers
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ReverseMap();
            CreateMap<Employee, EmployeeDto>()
                .ReverseMap();
        }
    }
}
