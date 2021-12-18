using Microsoft.AspNetCore.Identity;

namespace TimesheetAPI.Model.DbModels
{
    /// <summary>
    /// Custom Identity User Token che consente l'utilizzo di proprietà di navigazione non disponibili con l'IdentityUserToken di .NET Core 
    /// </summary>
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        /// <summary>
        /// La proprietà di navigazione ad ApplicationUser
        /// </summary>
        public virtual ApplicationUser User { get; set; }
    }
}
