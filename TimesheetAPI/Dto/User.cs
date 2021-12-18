// create user dto class in dto folder
using TimesheetAPI.Model.DbModels;

namespace TimesheetAPI.Dto
{
    public class User : ApplicationUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}