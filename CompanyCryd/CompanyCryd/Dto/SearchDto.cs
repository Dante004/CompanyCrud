using CompanyCrud.Models;
using System;

namespace CompanyCrud.Dto
{
    public class SearchDto
    {
        public string Keyword { get; set; }
        public DateTime EmployeeDateOfBirthFrom { get; set; }
        public DateTime EmployeeDateOfBirthTo { get; set; }
        public JobTitle EmployeeJobTitles { get; set; }
    }
}
