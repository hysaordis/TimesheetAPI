using System.Collections.Generic;
using TimesheetAPI.Model.DbModels;

namespace TimesheetAPI.Repositories.DBModels
{
    public class ActivityType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser Employee { get; set; }
        public virtual Timesheet Timesheet { get; set; }
    }
}