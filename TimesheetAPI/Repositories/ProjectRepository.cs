
using TimesheetAPI.api.Repositories;
using TimesheetAPI.Repositories.DBModels;

namespace TimesheetAPI.Repositories
{
    public class ProjectRepository : RawCRUDRepository<int, Project>
    {
        public ProjectRepository(MainContext Context) : base(Context)
        {
        }
    }
    public class EmployeeProjectRepository : RawCRUDRepository<int, EmployeeProject>
    {
        public EmployeeProjectRepository(MainContext Context) : base(Context)
        {
        }
    }
}