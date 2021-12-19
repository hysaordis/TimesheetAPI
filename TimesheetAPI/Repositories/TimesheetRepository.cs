
using TimesheetAPI.api.Repositories;
using TimesheetAPI.Repositories.DBModels;

namespace TimesheetAPI.Repositories
{
    public class TimesheetRepository : RawCRUDRepository<int, Timesheet>
    {
        public TimesheetRepository(MainContext Context) : base(Context)
        {
        }
    }
}