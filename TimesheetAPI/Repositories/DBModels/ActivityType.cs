using System.Collections.Generic;

namespace TimesheetAPI.Repositories.DBModels
{
    public class ActivityType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; }

        public ActivityType()
        {
            Timesheets = new List<Timesheet>();
        }

        public ActivityType(string description, int projectId)
        {
            Description = description;
            ProjectId = projectId;
            Timesheets = new List<Timesheet>();
        }

        public ActivityType(string description, int projectId, ICollection<Timesheet> timesheets)
        {
            Description = description;
            ProjectId = projectId;
            Timesheets = timesheets;
        }
    }
}