using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UsersCrud.Auth
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureDeleted();
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var adminId = "2288a9b4-35ee-4c13-b863-36184e701e0a";
            var adminUser = new IdentityUser
            {
                Id = adminId,
                UserName = "admin@gmail.com",
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
            };
            var passwordHasher = new PasswordHasher<IdentityUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin-123");

            builder.Entity<IdentityUser>().HasData(adminUser);

            var adminRoleId = "e1110812-5f76-4889-93a7-b4c677c2d8dd";
            var adminRole = "Admin";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = adminRole, NormalizedName = adminRole.ToUpper() }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = adminRoleId, UserId = adminId }
            );

            base.OnModelCreating(builder);
        }
    }
}
