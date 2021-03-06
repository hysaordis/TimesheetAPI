using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetAPI.Model.DbModels;
using TimesheetAPI.Models;
using TimesheetAPI.Repositories.DBModels;

namespace TimesheetAPI.Services.Interfaces
{
    public interface IProjectService
    {
        Task<List<Project>> GetProjects(ApplicationUser user);
        Task<Project> GetProjectById(ApplicationUser user, int id);
        Task<Project> CreateProject(Project project);
        Task<bool> UpdateProject(Project project);
        Task<bool> DeleteProject(Project project);

        // add Employee to Project
        Task<EmployeeProject> AddEmployeeToProject(ApplicationUser user, Project project);
    }
}