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
    public class TimesheetService : ITimesheetService
    {
        private readonly ICRUDRepository<int, Timesheet> _timesheetRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TimesheetService(ICRUDRepository<int, Timesheet> timesheetRepository,
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
            IQueryable<Timesheet> result;
            // var result = _timesheetRepository.GetAll().AsQueryable();
            var user = _userManager.FindByIdAsync(searchTimesheet.UserId).Result;
            // Get user roles of the user
            var roles = _userManager.GetRolesAsync(user).Result;
            if (!roles.Contains("Admin"))
            {
                result = _timesheetRepository.Filter(x => x.EmployeesId == searchTimesheet.UserId);
            }
            else
            {
                result = _timesheetRepository.GetAll().AsQueryable();
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