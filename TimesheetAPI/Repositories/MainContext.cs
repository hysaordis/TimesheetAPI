using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimesheetAPI.Model.DbModels;
using TimesheetAPI.Repositories.DBModels;

namespace TimesheetAPI.api.Repositories
{
    public class MainContext : IdentityDbContext<ApplicationUser,
            ApplicationRole,
            string,
            ApplicationUserClaim,
            ApplicationUserRole,
            ApplicationUserLogin,
            ApplicationRoleClaim,
            ApplicationUserToken>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        public MainContext(DbContextOptions<MainContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(user =>
            {
                // Each User can have many UserClaims
                user.HasMany(e => e.Claims)
                .WithOne(e => e.User)
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

                // Each User can have many UserLogins
                user.HasMany(e => e.Logins)
                .WithOne(e => e.User)
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

                // Each User can have many UserTokens
                user.HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

                // Each User can have many entries in the UserRole join table
                user.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

                // relationships between tables ApplicationUser and Project
                modelBuilder.Entity<EmployeeProject>().HasKey(sc => new { sc.ApplicationUserId, sc.ProjectId });
            });

            modelBuilder.Entity<ApplicationRole>(role =>
            {
                // Each Role can have many entries in the UserRole join table
                role.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                // Each Role can have many associated RoleClaims
                role.HasMany(e => e.RoleClaims)
                .WithOne(e => e.Role)
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();
            });

            this.SeedRoles(modelBuilder);
            this.SeedUsers(modelBuilder);
            this.SeedUserRoles(modelBuilder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new ApplicationRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
                );
        }
        private void SeedUserRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
                );
        }
        private void SeedUsers(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            ApplicationUser userAdmin = new ApplicationUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "Admin",
                NormalizedUserName = "Admin",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "Admin123"),
                Email = "admin@commit.it",
                NormalizedEmail = "admin@commit.it",
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
            };

            builder.Entity<ApplicationUser>().HasData(userAdmin);
        }
    }
}
