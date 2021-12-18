using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetAPI.Model.DbModels;
using TimesheetAPI.Models;
using TimesheetAPI.Repositories.DBModels;
using TimesheetAPI.Services.Filters;
using TimesheetAPI.Services.Interfaces.Filters;

namespace TimesheetAPI.Services.Interfaces
{
    public interface ITimesheetService
    {
        Task<Timesheet> AddOrUpdateTimesheet(Timesheet timesheet);
        Task<List<Timesheet>> SearchTimesheets(SearchTimesheet searchTimesheet);
    }
}