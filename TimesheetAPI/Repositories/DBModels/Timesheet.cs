using System;
using System.Collections.Generic;
using TimesheetAPI.Model.DbModels;

namespace TimesheetAPI.Repositories.DBModels
{
    public class Timesheet
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int ActivityTypeId { get; set; }
        public ActivityType ActivityType { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public ApplicationUser Employees { get; set; }
    }
}