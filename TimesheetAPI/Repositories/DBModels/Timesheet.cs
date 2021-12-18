using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TimesheetAPI.Model.DbModels;

namespace TimesheetAPI.Repositories.DBModels
{
    public class Timesheet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public Project Project { get; set; }

        [Required]
        public int ActivityTypeId { get; set; }

        [Required]
        public ActivityType ActivityType { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Hours { get; set; }

        [Required]
        public ApplicationUser Employees { get; set; }
    }
}