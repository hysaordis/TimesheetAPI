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
    public class ActivityTypeService : IActivityTypeService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICRUDRepository<int, ActivityType> _activityTypeRepository;
        public ActivityTypeService(ICRUDRepository<int, ActivityType> activityTypeRepository,
                                    UserManager<ApplicationUser> userManager)
        {
            _activityTypeRepository = activityTypeRepository;
            _userManager = userManager;
        }
        public Task<ActivityType> CreateActivityType(ActivityType activityType)
        {
            return Task.FromResult(_activityTypeRepository.Create(activityType));
        }

        public Task<ActivityType> DeleteActivityType(ActivityType activityType)
        {
            _activityTypeRepository.Delete(activityType);
            return Task.FromResult(activityType);
        }

        public Task<ActivityType> GetActivityTypeById(ApplicationUser user, int id)
        {
            return Task.FromResult(_activityTypeRepository.Find(id));
        }

        public Task<List<ActivityType>> GetActivityTypes(ApplicationUser user)
        {
            // Get user roles of the user
            var roles = _userManager.GetRolesAsync(user).Result;
            // If user is admin, return all projects
            if (roles.Contains("Admin"))
            {
                return Task.FromResult(_activityTypeRepository.GetAll().ToList());
            }
            else
            {
                return Task.FromResult(_activityTypeRepository.Filter(x => x.Project.EmployeeProjects.Any(y => y.ApplicationUserId == user.Id)).ToList());
            }
        }

        public Task<ActivityType> UpdateActivityType(ActivityType activityType)
        {
            _activityTypeRepository.Update(activityType);
            return Task.FromResult(activityType);
        }
    }
}