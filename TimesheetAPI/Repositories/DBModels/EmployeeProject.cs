using TimesheetAPI.Model.DbModels;

namespace TimesheetAPI.Repositories.DBModels
{
    // create join table ApplicationUser-Project
    public class EmployeeProject
    {
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}