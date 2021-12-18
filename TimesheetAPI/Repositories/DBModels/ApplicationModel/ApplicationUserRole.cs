using Microsoft.AspNetCore.Identity;

namespace TimesheetAPI.Model.DbModels
{
    /// <summary>
    /// Custom Identity User Role che consente l'utilizzo di proprietà di navigazione non disponibili con l'IdentityUserRole di .NET Core 
    /// </summary>
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        /// <summary>
        /// La proprietà di navigazione ad ApplicationUser
        /// </summary>
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// La proprietà di navigazione ad ApplicationRole
        /// </summary>
        public virtual ApplicationRole Role { get; set; }
    }
}
