using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPMS.API.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProjectManagerId { get; set; }
        public Employee ProjectManager { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}