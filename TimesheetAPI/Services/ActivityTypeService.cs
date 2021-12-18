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

        public Task<ActivityType> AssignProjectToActivityType(ActivityType activityType, Project project)
        {
            if (activityType == null || project == null)
            {
                return null;
            }

            activityType.Project = project;
            // save to db
            _activityTypeRepository.Update(activityType);

            return Task.FromResult(activityType);
        }

        public Task<ActivityType> AddTimesheetToActivityType(ActivityType activityType, Timesheet timesheet)
        {
            if (activityType == null || timesheet == null)
            {
                return null;
            }

            activityType.Timesheets.Add(timesheet);
            // save to db
            _activityTypeRepository.Update(activityType);

            return Task.FromResult(activityType);
        }

        public Task<ActivityType> CreateActivityType(ActivityType activityType)
        {
            if (activityType == null)
            {
                return null;
            }

            // save to db
            _activityTypeRepository.Create(activityType);

            return Task.FromResult(activityType);
        }

        public Task<ActivityType> DeleteActivityType(ActivityType activityType)
        {
            if (activityType == null)
            {
                return null;
            }

            // save to db
            _activityTypeRepository.Delete(activityType);

            return Task.FromResult(activityType);
        }

        public Task<ActivityType> GetActivityTypeById(ApplicationUser user, int id)
        {
            if (user == null)
            {
                return null;
            }

            var activityType = _activityTypeRepository.Find(id);

            return Task.FromResult(activityType);
        }

        public Task<List<ActivityType>> GetActivityTypes(ApplicationUser user)
        {
            if (user == null)
            {
                return null;
            }

            var activityTypes = _activityTypeRepository.GetAll();

            return Task.FromResult(activityTypes.ToList());
        }

        public Task<ActivityType> RemoveProjectFromActivityType(ActivityType activityType, Project project)
        {
            if (activityType == null || project == null)
            {
                return null;
            }

            activityType.Project = null;
            // save to db
            _activityTypeRepository.Update(activityType);

            return Task.FromResult(activityType);
        }

        public Task<ActivityType> RemoveTimesheetFromActivityType(ActivityType activityType, Timesheet timesheet)
        {
            if (activityType == null || timesheet == null)
            {
                return null;
            }

            activityType.Timesheets.Remove(timesheet);
            // save to db
            _activityTypeRepository.Update(activityType);

            return Task.FromResult(activityType);
        }

        public Task<ActivityType> UpdateActivityType(ActivityType activityType)
        {
            if (activityType == null)
            {
                return null;
            }

            // save to db
            _activityTypeRepository.Update(activityType);

            return Task.FromResult(activityType);
        }
    }
}