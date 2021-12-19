
using TimesheetAPI.api.Repositories;
using TimesheetAPI.Repositories.DBModels;

namespace TimesheetAPI.Repositories
{
    public class ActivityTypeRepository : RawCRUDRepository<int, ActivityType>
    {
        public ActivityTypeRepository(MainContext Context) : base(Context)
        {
        }
    }
}