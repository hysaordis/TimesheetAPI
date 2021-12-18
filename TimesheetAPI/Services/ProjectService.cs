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

        public Task<Project> AddActivityTypeToProject(Project project, ActivityType activityType)
        {
            if (project == null || activityType == null)
            {
                return null;
            }

            project.ActivityTypes.Add(activityType);
            return Task.FromResult(project);
        }

        public Task<Project> AddEmployeeToProject(Project project, ApplicationUser employee)
        {
            if (project == null || employee == null)
            {
                return null;
            }

            project.Employees.Add(employee);
            return Task.FromResult(project);
        }

        public Task<Project> AddTimesheetToProject(Project project, Timesheet timesheet)
        {
            if (project == null || timesheet == null)
            {
                return null;
            }

            project.Timesheets.Add(timesheet);
            return Task.FromResult(project);
        }

        public Task<Project> CreateProject(Project project)
        {
            if (project == null)
            {
                return null;
            }

            _projectRepository.Create(project);
            return Task.FromResult(project);
        }

        public Task<Project> DeleteProject(Project project)
        {
            if (project == null)
            {
                return null;
            }

            _projectRepository.Delete(project);
            return Task.FromResult(project);
        }

        public Task<Project> GetProjectById(ApplicationUser user, int id)
        {
            if (user == null)
            {
                return null;
            }

            var project = _projectRepository.Find(id);
            return Task.FromResult(project);
        }

        public Task<List<Project>> GetProjects(ApplicationUser user)
        {
            if (user == null)
            {
                return null;
            }

            var projects = _projectRepository.GetAll();
            return Task.FromResult(projects.ToList());
        }

        public Task<Project> RemoveActivityTypeFromProject(Project project, ActivityType activityType)
        {
            if (project == null || activityType == null)
            {
                return null;
            }

            project.ActivityTypes.Remove(activityType);
            return Task.FromResult(project);
        }

        public Task<Project> RemoveEmployeeFromProject(Project project, ApplicationUser employee)
        {
            if (project == null || employee == null)
            {
                return null;
            }

            project.Employees.Remove(employee);
            return Task.FromResult(project);
        }

        public Task<Project> RemoveTimesheetFromProject(Project project, Timesheet timesheet)
        {
            if (project == null || timesheet == null)
            {
                return null;
            }

            project.Timesheets.Remove(timesheet);
            return Task.FromResult(project);
        }

        public Task<Project> UpdateProject(Project project)
        {
            if (project == null)
            {
                return null;
            }

            _projectRepository.Update(project);
            return Task.FromResult(project);
        }
    }
}