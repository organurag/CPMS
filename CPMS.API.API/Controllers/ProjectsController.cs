using CPMS.API.DAL.ProjectDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CPMS.API.API.Controllers
{
    [System.Web.Http.Authorize]
    public class ProjectsController : ApiController
    {
        private readonly ProjectDAL _projectDAL;

        public ProjectsController(ProjectDAL projectDAL)
        {
            _projectDAL = projectDAL;
        }

        public IHttpActionResult GetProjects(int page = 1, int pageSize = 10)
        {
            var totalItems = _projectDAL.GetTotalProjectCount();

            var projects = _projectDAL.GetProjects(page, pageSize);

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
                hasMore = (page * pageSize) < totalItems,
                totalItems
            };

            return Ok(result);
        }
    }

}