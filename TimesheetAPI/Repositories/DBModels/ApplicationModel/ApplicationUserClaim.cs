using Microsoft.AspNetCore.Identity;

namespace TimesheetAPI.Model.DbModels
{
    /// <summary>
    /// Custom Identity User Claim che consente l'utilizzo di proprietà di navigazione non disponibili con l'IdentityUserClaim di .NET Core 
    /// </summary>
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        /// <summary>
        /// La proprietà di navigazione ad ApplicationUser
        /// </summary>
        public virtual ApplicationUser User { get; set; }
    }
}
