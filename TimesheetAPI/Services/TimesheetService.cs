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
    public class TimesheetService : ITimesheetService
    {
        private readonly RawCRUDRepository<int, Timesheet> _timesheetRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TimesheetService(RawCRUDRepository<int, Timesheet> timesheetRepository,
                                 UserManager<ApplicationUser> userManager)
        {
            _timesheetRepository = timesheetRepository;
            _userManager = userManager;
        }

        public Task<Timesheet> AddOrUpdateTimesheet(Timesheet timesheet)
        {
            var result = _timesheetRepository.Find(timesheet.Id);
            if (result == null)
            {
                result = _timesheetRepository.Create(timesheet);
            }
            else
            {
                _timesheetRepository.Update(timesheet);
            }
            return Task.FromResult(result);
        }

        public Task<List<Timesheet>> SearchTimesheets(SearchTimesheetQuery searchTimesheet)
        {
            var result = _timesheetRepository.GetAll().AsQueryable();
            var user = _userManager.FindByIdAsync(searchTimesheet.UserId).Result;
            if (!user.UserRoles.Any(x => x.Role.Name == "Admin"))
            {
                result = result.Where(x => x.Employees.Id == searchTimesheet.UserId);
            }

            if (searchTimesheet.DateFrom != null)
            {
                result = result.Where(x => x.Date >= searchTimesheet.DateFrom);
            }
            if (searchTimesheet.DateTo != null)
            {
                result = result.Where(x => x.Date <= searchTimesheet.DateTo);
            }
            // Add search by project
            if (searchTimesheet.ProjectId != null)
            {
                result = result.Where(x => x.ProjectId == searchTimesheet.ProjectId);
            }
            // Add search by activity type
            if (searchTimesheet.ActivityTypeId != null)
            {
                result = result.Where(x => x.ActivityTypeId == searchTimesheet.ActivityTypeId);
            }
            // add search by page number and page size
            if (searchTimesheet.Page != null)
            {
                result = result.Skip((searchTimesheet.Page - 1) * 10).Take(10);
            }

            return Task.FromResult(result.ToList());
        }
    }
}