using System;

namespace CompanyCrud.Models
{
    public enum JobTitle
    {
        Administrator,
        Developer,
        Architect,
        Manager
    }

    public class Employee
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public JobTitle JobTitle { get; set; }
        public Company Company { get; set; }
    }
}
