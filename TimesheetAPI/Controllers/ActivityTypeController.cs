using Microsoft.AspNetCore.Mvc;
using TimesheetAPI.Services.Interfaces;
using TimesheetAPI.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TimesheetAPI.Repositories.DBModels;
using Microsoft.AspNetCore.Identity;
using TimesheetAPI.Model.DbModels;

namespace TimesheetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivityTypeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IActivityTypeService _activityTypeService;

        public ActivityTypeController(UserManager<ApplicationUser> userManager,
                                        SignInManager<ApplicationUser> signInManager,
                                        IActivityTypeService activityTypeService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _activityTypeService = activityTypeService;
        }

        [HttpPost]
        [Route("api/activityType/add")]
        public async Task<IActionResult> AddActivityTyp([FromBody] ActivityType activityType)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _activityTypeService.CreateActivityType(activityType);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/activityType/getAll")]
        public async Task<IActionResult> GetAllActivityTypes()
        {
            var user = await _userManager.GetUserAsync(User);
            var activityTypes = await _activityTypeService.GetActivityTypes(user);
            return Ok(activityTypes);
        }

        [HttpGet]
        [Route("api/activityType/get/{id}")]
        public async Task<IActionResult> GetActivityType(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();
            var activityType = await _activityTypeService.GetActivityTypeById(user, id);
            return Ok(activityType);
        }

        [HttpPut]
        [Route("api/activityType/update/{id}")]
        public async Task<IActionResult> UpdateActivityType([FromBody] ActivityType activityType)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();
            var result = await _activityTypeService.UpdateActivityType(activityType);
            return Ok(result);
        }

        [HttpDelete]
        [Route("api/activityType/delete/{id}")]
        public async Task<IActionResult> DeleteActivityType(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();
            // get activity type by id
            var activityType = await _activityTypeService.GetActivityTypeById(user, id);
            // if activity type is null, return not found
            if (activityType == null)
                return NotFound();

            var result = await _activityTypeService.DeleteActivityType(activityType);
            return Ok(result);
        }
    }
}