using CPMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Data.Entity;


namespace CPMS.Controllers
{
    public class ProjectsController : ApiController
    {
        private ProjectContext _context;

        public ProjectsController()
        {
            _context = new ProjectContext();
        }

        public IHttpActionResult GetProjects(int page = 1, int pageSize = 10)
        {
            var totalItems = _context.Projects.Count(); // Get total number of projects

            var projects = _context.Projects
                .Include(p => p.ProjectManager)
                .Include(p => p.Employees.Select(e => e.Department))
                .OrderBy(p => p.ProjectId) // Assuming ProjectId is the unique identifier for projects
                .Skip((page - 1) * pageSize) // Skip the required number of items based on the page number and page size
                .Take(pageSize) // Take the specified number of items for the current page
                .ToList();

            var projectList = projects.Select(p => new
            {
                ProjectName = p.Name,
                ProjectStartDate = p.StartDate,
                ProjectEndDate = p.EndDate,
                ProjectManagerName = p.ProjectManager?.Name,
                ProjectManagerEmail = p.ProjectManager?.Email,
                Employees = p.Employees.Select(e => new
                {
                    Name = e.Name,
                    Email = e.Email,
                    DOJ = e.DateOfJoining,
                    DepartmentName = e.Department?.Name
                }).ToList()
            }).ToList();

            var result = new
            {
                data = projectList,
                page,
                pageSize,
                hasMore = (page * pageSize) < totalItems, // Determine if there are more items to be paginated
                totalItems
            };

            return Ok(result);
        }
    }
}