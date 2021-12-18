using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetAPI.Dto;
using TimesheetAPI.Repositories.DBModels;

namespace TimesheetAPI.Services.Interfaces
{
    public interface ITimesheetService
    {
        Task<Timesheet> AddOrUpdateTimesheet(Timesheet timesheet);
        Task<List<Timesheet>> SearchTimesheets(SearchTimesheetQuery searchTimesheet);
    }
}