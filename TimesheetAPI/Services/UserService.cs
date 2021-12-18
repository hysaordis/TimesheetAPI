using System.Security.AccessControl;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimesheetAPI.api.ConfigurationModels;
using TimesheetAPI.Exception;
using TimesheetAPI.Model.DbModels;
using TimesheetAPI.Models;
using TimesheetAPI.Services.Interfaces;

namespace TimesheetAPI.api.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly JwtConfiguration _jwtConfiguration;


        public UserService(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            JwtConfiguration jwtConfiguration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtConfiguration = jwtConfiguration;
        }

        // create method for add user to database
        public async Task<ApplicationUser> Register(ApiAuthenticateModel model)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                NormalizedUserName = model.UserName.ToUpper(),
                Email = model.UserName,
                NormalizedEmail = model.UserName.ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return user;
            }
            else
            {
                throw new HttpStatusCodeException(400, result.Errors.First().Description);
            }
        }

        // create method for get list of users with role "User"
        public async Task<List<ApplicationUser>> GetUsers()
        {
            var users = await _userManager.Users.Where(u => u.UserRoles.Any(r => r.RoleId == "User")).ToListAsync();
            return users;
        }

        // create method for get user by name
        public async Task<ApplicationUser> GetUserByName(string userName)
        {
            var user = await _userManager.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
            return user;
        }

        // create method for update user in database
        public async Task<ApplicationUser> Update(ApiAuthenticateModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                throw new HttpStatusCodeException(400, "User not found");
            }
            user.UserName = model.UserName;
            user.NormalizedUserName = model.UserName.ToUpper();
            user.Email = model.UserName;
            user.NormalizedEmail = model.UserName.ToUpper();
            user.EmailConfirmed = true;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.LockoutEnabled = false;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                throw new HttpStatusCodeException(400, result.Errors.First().Description);
            }
        }

        // create method for delete user in database
        public async Task<ApplicationUser> Delete(ApiAuthenticateModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                throw new HttpStatusCodeException(400, "User not found");
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                throw new HttpStatusCodeException(400, result.Errors.First().Description);
            }
        }

        public async Task<JwtToken> Authenticate(ApiAuthenticateModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.Include(u => u.UserRoles).FirstOrDefault(r => r.UserName == model.UserName);
                var userRoles = appUser?.UserRoles?.ToList();
                string accessToken = string.Empty;

                return new JwtToken()
                {
                    AuthToken = GenerateJwtToken(appUser.UserName, userRoles, appUser)
                };
            }

            throw new HttpStatusCodeException(401, "Could not authenticate user");
        }

        private string GenerateJwtToken(string userName, IEnumerable<ApplicationUserRole> userRoles, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            if (userRoles != null && userRoles.Any())
            {
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role?.Role?.Name));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtConfiguration.JwtExpireDays));

            var token = new JwtSecurityToken(
                _jwtConfiguration.JwtIssuer,
                _jwtConfiguration.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
