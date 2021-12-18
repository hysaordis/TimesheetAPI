using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TimesheetAPI.api.ConfigurationModels;
using TimesheetAPI.api.Repositories;
using TimesheetAPI.api.Services;
using TimesheetAPI.Model.DbModels;
using TimesheetAPI.Services.Interfaces;
using System.Text.Json;

namespace TimesheetAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // aggiungi JwtConfiguration
            services.Configure<JwtConfiguration>(Configuration.GetSection("JwtConfiguration"));
            var jwtConfiguration = new JwtConfiguration();
            Configuration.Bind("JwtConfiguration", jwtConfiguration);
            services.AddSingleton(jwtConfiguration);

            services.AddMvc();

            // add dbcontext
            services.AddDbContext<MainContext>(options => options.UseMySql(Configuration["DatabaseConnection"]));
            // add identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Stores.MaxLengthForKeys = 128;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<MainContext>().AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtConfiguration.JwtIssuer,
                        ValidAudience = jwtConfiguration.JwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.JwtKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            // Agiungiamo le dipendence
            services.AddScoped<IUserService, UserService>();


            //  ignorare il loop delle reference al infinito
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action}/{id?}");
            });

            // agiungiamo le migration se ci sono
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MainContext>();
                context.Database.Migrate();
            }
        }
    }
}
