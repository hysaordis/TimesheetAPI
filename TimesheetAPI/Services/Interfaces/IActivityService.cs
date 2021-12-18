using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetAPI.Model.DbModels;
using TimesheetAPI.Models;
using TimesheetAPI.Repositories.DBModels;

namespace TimesheetAPI.Services.Interfaces
{
    // 
    public interface IActivityTypeService
    {
        Task<List<ActivityType>> GetActivityTypes(ApplicationUser user);
        Task<ActivityType> GetActivityTypeById(ApplicationUser user, int id);
        Task<ActivityType> CreateActivityType(ActivityType activityType);
        Task<ActivityType> UpdateActivityType(ActivityType activityType);
        Task<ActivityType> DeleteActivityType(ActivityType activityType);
    }
}