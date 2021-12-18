// implement IProjectService in ProjectService.cs:
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetAPI.api.Repositories;
using TimesheetAPI.Model.DbModels;
using TimesheetAPI.Models;
using TimesheetAPI.Repositories.DBModels;
using TimesheetAPI.Services.Interfaces;

namespace TimesheetAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly RawCRUDRepository<int, Project> _projectRepository;
        private readonly RawCRUDRepository<int, ActivityType> _activityTypeRepository;
        private readonly RawCRUDRepository<int, Timesheet> _timesheetRepository;

        public ProjectService(RawCRUDRepository<int, Project> projectRepository,
            RawCRUDRepository<int, ActivityType> activityTypeRepository,
            RawCRUDRepository<int, Timesheet> timesheetRepository)
        {
            _projectRepository = projectRepository;
            _activityTypeRepository = activityTypeRepository;
            _timesheetRepository = timesheetRepository;
        }

        public Task<Project> CreateProject(Project project)
        {
            throw new System.NotImplementedException();
        }

        public Task<Project> DeleteProject(Project project)
        {
            throw new System.NotImplementedException();
        }

        public Task<Project> GetProjectById(ApplicationUser user, int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Project>> GetProjects(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task<Project> UpdateProject(Project project)
        {
            throw new System.NotImplementedException();
        }
    }
}