using CPMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS.API.DAL.ProjectDAL
{
    public interface IProjectDAL
    {
        List<Project> GetProjects(int page = 1, int pageSize = 10);
    }
}
