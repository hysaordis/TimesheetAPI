using System;
using System.Collections.Generic;

namespace TimesheetAPI.Repositories.DBModels
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Todo : convert to unix time stamp long
        public DateTime StartDate { get; set; }
        // Todo : convert to unix time stamp long
        public DateTime EndDate { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; }
        public virtual ICollection<ActivityType> ActivityTypes { get; set; }
    }
}