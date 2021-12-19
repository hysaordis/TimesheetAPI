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
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProjectController(UserManager<ApplicationUser> userManager, IProjectService projectService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _projectService = projectService;
            _signInManager = signInManager;
        }

        // add employee to project (admin only)
        [HttpPost("api/project/addEmployeeToProject")]
        public async Task<IActionResult> AddEmployeeToProject([FromBody] Project project)
        {
            // ToDo : Crate ProjectDto and add validation

            var user = await _userManager.GetUserAsync(User);
            var employeeProject = new EmployeeProject
            {
                ApplicationUserId = user.Id,
                ProjectId = project.Id
            };
            var result = await _projectService.AddEmployeeToProject(user, project);
            return Ok(result);
        }

        [HttpGet]
        [Route("projects")]
        public async Task<IActionResult> GetAllProjects()
        {
            // get user from token
            var user = await _userManager.GetUserAsync(User);
            // check if user is logged in
            if (user == null)
                return Unauthorized();

            var projects = await _projectService.GetProjects(user);
            return Ok(projects);
        }

        [HttpGet]
        [Route("projects/{id}")]
        // authorise admin and user
        public async Task<IActionResult> GetProjectById(int id)
        {
            // get user from token
            var user = await _userManager.GetUserAsync(User);
            // check if user is logged in
            if (user == null)
                return Unauthorized();
            var project = await _projectService.GetProjectById(user, id);
            return Ok(project);
        }

        [HttpPost]
        [Route("projects")]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            // ToDo : Crate ProjectDto and add validation

            var newProject = await _projectService.CreateProject(project);
            return Ok(newProject);
        }

        [HttpPut]
        [Route("projects/{id}")]
        public async Task<IActionResult> UpdateProject([FromBody] Project project)
        {
            // ToDo : Crate ProjectDto and add validation

            var updatedProject = await _projectService.UpdateProject(project);
            return Ok(updatedProject);
        }

        [HttpDelete]
        [Route("projects/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            // get user from token
            var user = await _userManager.GetUserAsync(User);
            // check if user is logged in
            if (user == null)
                return Unauthorized();
            var project = await _projectService.GetProjectById(user, id);
            // if project is not found, return 404
            if (project == null)
                return NotFound();
            await _projectService.DeleteProject(project);
            return Ok();
        }
    }
}