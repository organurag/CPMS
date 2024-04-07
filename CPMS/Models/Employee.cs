using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPMS.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfJoining { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        
        public virtual ICollection<Project> Projects { get; set; }
    }
}