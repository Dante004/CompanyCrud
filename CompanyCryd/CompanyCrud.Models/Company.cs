using System.Collections.Generic;

namespace CompanyCrud.Models
{
    public class Company
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public int Establishment { get; set; }
        public ICollection<Employee> Employes { get; set; }
    }
}
