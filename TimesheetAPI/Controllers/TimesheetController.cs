using Microsoft.AspNetCore.Mvc;
using TimesheetAPI.Services.Interfaces;
using TimesheetAPI.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TimesheetAPI.Repositories.DBModels;
using Microsoft.AspNetCore.Identity;
using TimesheetAPI.Model.DbModels;
using TimesheetAPI.Dto;

namespace TimesheetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimesheetController : ControllerBase
    {
        private readonly ITimesheetService _timesheetService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TimesheetController(UserManager<ApplicationUser> userManager, ITimesheetService timesheetService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _timesheetService = timesheetService;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("addorupdate")]
        public async Task<IActionResult> AddTimesheet([FromBody] Timesheet timesheet)
        {
            // ToDo : Add validation for timesheet
            // Todo : Add Owner validation for timesheet
            // Todo : Create TimesheetDto and map it to Timesheet
            var user = await _userManager.GetUserAsync(User);
            var result = await _timesheetService.AddOrUpdateTimesheet(timesheet);
            return Ok(result);
        }

        // SearchTimesheet 
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchTimesheet([FromBody] SearchTimesheetQuery searchquery)
        {
            // get user id from token and pass it to search query
            var user = await _userManager.GetUserAsync(User);
            searchquery.UserId = user.Id;

            var result = await _timesheetService.SearchTimesheets(searchquery);
            return Ok(result);
        }
    }
}
