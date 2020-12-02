using CompanyCrud.Models;
using System.Collections.Generic;

namespace CompanyCrud.Dto
{
    public class CompanyDto
    {
        public string Name { get; set; }
        public int Establishment { get; set; }
        public ICollection<EmployeeDto> Employes { get; set; }
    }
}
