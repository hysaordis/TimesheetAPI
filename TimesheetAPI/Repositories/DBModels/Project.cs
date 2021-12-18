using System;
using System.Collections.Generic;
using TimesheetAPI.Model.DbModels;

namespace TimesheetAPI.Repositories.DBModels
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual ICollection<ApplicationUser> Employees { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; }
        public virtual ICollection<ActivityType> ActivityTypes { get; set; }

        public Project()
        {
            Employees = new List<ApplicationUser>();
            Timesheets = new List<Timesheet>();
            ActivityTypes = new List<ActivityType>();
        }

        public Project(string name, string description, DateTime startDate, DateTime endDate, ICollection<ApplicationUser> employees, ICollection<Timesheet> timesheets, ICollection<ActivityType> activityTypes)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Employees = employees;
            Timesheets = timesheets;
            ActivityTypes = activityTypes;
        }

        public Project(string name, string description, DateTime startDate, DateTime endDate)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Employees = new List<ApplicationUser>();
            Timesheets = new List<Timesheet>();
            ActivityTypes = new List<ActivityType>();
        }
    }
}