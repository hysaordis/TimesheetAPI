// implement IActivityTypeService in ActivityTypeService.cs:
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
    public class ActivityTypeService : IActivityTypeService
    {
        private readonly RawCRUDRepository<int, ActivityType> _activityTypeRepository;
        private readonly RawCRUDRepository<int, Project> _projectRepository;
        private readonly RawCRUDRepository<int, Timesheet> _timesheetRepository;

        public ActivityTypeService(RawCRUDRepository<int, ActivityType> activityTypeRepository,
            RawCRUDRepository<int, Project> projectRepository,
            RawCRUDRepository<int, Timesheet> timesheetRepository)
        {
            _activityTypeRepository = activityTypeRepository;
            _projectRepository = projectRepository;
            _timesheetRepository = timesheetRepository;
        }

        public Task<ActivityType> CreateActivityType(ActivityType activityType)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActivityType> DeleteActivityType(ActivityType activityType)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActivityType> GetActivityTypeById(ApplicationUser user, int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<ActivityType>> GetActivityTypes(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActivityType> UpdateActivityType(ActivityType activityType)
        {
            throw new System.NotImplementedException();
        }
    }
}