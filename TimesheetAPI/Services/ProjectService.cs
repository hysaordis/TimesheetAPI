using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetAPI.api.Repositories;
using TimesheetAPI.Dto;
using TimesheetAPI.Model.DbModels;
using TimesheetAPI.Repositories.DBModels;
using TimesheetAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace TimesheetAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly RawCRUDRepository<int, Project> _projectRepository;
        private readonly RawCRUDRepository<int, EmployeeProject> _employeeProjectRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProjectService(RawCRUDRepository<int, Project> projectRepository,
                                UserManager<ApplicationUser> userManager,
                                RawCRUDRepository<int, EmployeeProject> employeeProjectRepository)
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
            // If user is admin, return all projects
            if (user.UserRoles.Any(x => x.Role.Name == "Admin"))
            {
                return Task.FromResult(_projectRepository.GetAll().ToList());
            }
            // If user is not admin, return only projects that user is assigned to
            else
            {
                return Task.FromResult(_projectRepository.Filter(x => x.EmployeeProjects.Any(y => y.ApplicationUser.Id == user.Id)).ToList());
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