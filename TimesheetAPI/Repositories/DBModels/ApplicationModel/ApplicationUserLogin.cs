using Microsoft.AspNetCore.Identity;


namespace TimesheetAPI.Model.DbModels
{
    /// <summary>
    /// Custom Identity User Login che consente l'utilizzo di proprietà di navigazione non disponibili con l'IdentityUserLogin di .NET Core 
    /// </summary>
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        /// <summary>
        /// La proprietà di navigazione ad ApplicationUser
        /// </summary>
        public virtual ApplicationUser User { get; set; }
    }
}
