using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetAPI.Model.DbModels;
using TimesheetAPI.Models;
using TimesheetAPI.Repositories.DBModels;

namespace TimesheetAPI.Services.Interfaces
{
    // create crud interface for ITimesheetService
    public interface ITimesheetService
    {
        Task<List<Timesheet>> GetTimesheets(ApplicationUser user);
        Task<Timesheet> GetTimesheetById(ApplicationUser user, int id);
        Task<Timesheet> CreateTimesheet(Timesheet timesheet);
        Task<Timesheet> UpdateTimesheet(Timesheet timesheet);
        Task<Timesheet> DeleteTimesheet(Timesheet timesheet);
    }
}