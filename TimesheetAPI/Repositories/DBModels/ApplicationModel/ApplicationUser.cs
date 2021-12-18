using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using TimesheetAPI.Repositories.DBModels;

namespace TimesheetAPI.Model.DbModels
{
    /// <summary>
    /// Custom Identity User che consente l'utilizzo di proprietà di navigazione non disponibili con l'IdentityUser di .NET Core 
    /// </summary>
    public class ApplicationUser : IdentityUser<string>
    {
        /// <summary>
        /// La proprietà di navigazione ad ApplicationUserRole
        /// </summary>
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        /// <summary>
        /// La proprietà di navigazione ad ApplicationUserLogin
        /// </summary>
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }

        /// <summary>
        /// La proprietà di navigazione ad ApplicationUserToken
        /// /// </summary>
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }

        /// <summary>
        /// La proprietà di navigazion a ApplicationUserClaim
        /// </summary>
        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }

        /// <summary>
        /// Inizializza un nuovo ApplicationUser
        /// </summary>
        public ApplicationUser() { }

        /// <summary>
        /// Inizializza un nuovo ApplicationUser
        /// </summary>
        /// <param name="userRoles">I ruoli associati</param>
        public ApplicationUser(ICollection<ApplicationUserRole> userRoles)
        {
            UserRoles = userRoles;
        }

        // add other properties for business requirements
        public virtual ICollection<ActivityType> ActivityTypes { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
