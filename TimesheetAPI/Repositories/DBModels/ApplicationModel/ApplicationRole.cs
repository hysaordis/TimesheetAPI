using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TimesheetAPI.Model.DbModels
{
    /// <summary>
    /// Custom Identity Role che consente l'utilizzo di proprietà di navigazione non disponibili con l'IdentityRole di .NET Core 
    /// </summary>
    public class ApplicationRole : IdentityRole<string>
    {
        /// <summary>
        /// La proprietà di navigazione a ApplicationUserRoles
        /// </summary>
        public ICollection<ApplicationUserRole> UserRoles { get; set; }

        /// <summary>
        /// La proprietà di navigazione a to RoleClaims
        /// </summary>
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// Inizializza un nuovo ApplicationRole
        /// </summary>
        public ApplicationRole() { }

        /// <summary>
        /// Inizializza un nuovo ApplicationRole
        /// </summary>
        /// <param name="roleName">Il nome del ruolo</param>
        public ApplicationRole(string roleName) : base(roleName) { }
    }
}
