using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetAPI.api.Repositories;
using TimesheetAPI.Dto;
using TimesheetAPI.Model.DbModels;
using TimesheetAPI.Repositories.DBModels;
using TimesheetAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using TimesheetAPI.Repositories;
using TimesheetAPI.api.Repositories.Interfaces;

namespace TimesheetAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ICRUDRepository<int, Project> _projectRepository;
        private readonly ICRUDRepository<int, EmployeeProject> _employeeProjectRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectService(ICRUDRepository<int, Project> projectRepository,
                                UserManager<ApplicationUser> userManager,
                                ICRUDRepository<int, EmployeeProject> employeeProjectRepository)
        {
            _projectRepository = projectRepository;
            _userManager = userManager;
            _employeeProjectRepository = employeeProjectRepository;
        }

        public Task<EmployeeProject> AddEmployeeToProject(ApplicationUser user, Project project)
        {
            var employeeProject = new EmployeeProject
            {
                ApplicationUserId = user.Id,
                ProjectId = project.Id
            };
            var result = _employeeProjectRepository.Create(employeeProject);
            return Task.FromResult(result);
        }

        public Task<Project> CreateProject(Project project)
        {
            return Task.FromResult(_projectRepository.Create(project));
        }

        public Task<bool> DeleteProject(Project project)
        {
            _projectRepository.Delete(project);
            return Task.FromResult(true);
        }

        public Task<Project> GetProjectById(ApplicationUser user, int id)
        {
            return Task.FromResult(_projectRepository.Find(id));
        }

        public Task<List<Project>> GetProjects(ApplicationUser user)
        {
            // Get user roles of the user
            var roles = _userManager.GetRolesAsync(user).Result;
            // If user is admin, return all projects
            if (roles.Contains("Admin"))
            {
                return Task.FromResult(_projectRepository.GetAll().ToList());
            }
            // If user is not admin, return only projects that user is assigned to
            else
            {
                var employeeProjects = _employeeProjectRepository.Filter(x => x.ApplicationUserId == user.Id).ToList();
                // Get all projects that user is assigned to
                var projects = employeeProjects.Select(x => x.Project).ToList();
                return Task.FromResult(projects);
            }
        }

        public Task<bool> UpdateProject(Project project)
        {
            var existingProject = _projectRepository.Find(project.Id);
            if (existingProject != null)
            {
                existingProject.Name = project.Name;
                existingProject.Description = project.Description;
                existingProject.StartDate = project.StartDate;
                existingProject.EndDate = project.EndDate;
                existingProject.EmployeeProjects = project.EmployeeProjects;
                _projectRepository.Update(existingProject);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}