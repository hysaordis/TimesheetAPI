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
        Task<Project> UpdateProject(Project project);
        Task<Project> DeleteProject(Project project);
        // add Employee to Project
        Task<Project> AddEmployeeToProject(Project project, ApplicationUser employee);
        // remove Employee from Project
        Task<Project> RemoveEmployeeFromProject(Project project, ApplicationUser employee);
        // add ActivityType to Project
        Task<Project> AddActivityTypeToProject(Project project, ActivityType activityType);
        // remove ActivityType from Project
        Task<Project> RemoveActivityTypeFromProject(Project project, ActivityType activityType);
        // add Timesheet to Project
        Task<Project> AddTimesheetToProject(Project project, Timesheet timesheet);
        // remove Timesheet from Project
        Task<Project> RemoveTimesheetFromProject(Project project, Timesheet timesheet);
    }
}