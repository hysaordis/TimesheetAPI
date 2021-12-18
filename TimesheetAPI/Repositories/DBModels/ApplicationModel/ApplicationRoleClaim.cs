using Microsoft.AspNetCore.Identity;

namespace TimesheetAPI.Model.DbModels
{
    /// <summary>
    /// Custom Identity Role CLaim che consente l'utilizzo di proprietà di navigazione non disponibili con l'IdentityRoleClaim di .NET Core 
    /// </summary>
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        /// <summary>
        /// La proprietà di navigazione all'ApplicationRole
        /// </summary>
        public virtual ApplicationRole Role { get; set; }
    }
}
