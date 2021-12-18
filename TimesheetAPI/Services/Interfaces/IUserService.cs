using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetAPI.Model.DbModels;
using TimesheetAPI.Models;

namespace TimesheetAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<JwtToken> Authenticate(ApiAuthenticateModel model);
        Task<ApplicationUser> Delete(ApiAuthenticateModel model);
        Task<ApplicationUser> GetUserByName(string userName);
        Task<List<ApplicationUser>> GetUsers();
        Task<ApplicationUser> Register(ApiAuthenticateModel model);
        Task<ApplicationUser> Update(ApiAuthenticateModel model);
    }
}
