using CPMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CPMS.API.DAL.ProjectDAL
{
    public class ProjectDAL : IProjectDAL
    {
        private readonly ProjectContext _context;

        public ProjectDAL()
        {
            _context = new ProjectContext();
        }
        public List<Project> GetProjects(int page, int pageSize)
        {
            return _context.Projects
                .Include(p => p.ProjectManager)
                .Include(p => p.Employees.Select(e => e.Department))
                .OrderBy(p => p.ProjectId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalProjectCount()
        {
            return _context.Projects.Count();
        }
    }
}
