using Azure;
using Microsoft.EntityFrameworkCore;
using UsersCrud.DAL.Entities;

namespace UsersCrud.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var userRole = new Role { Id = Guid.Parse("2dc2c800-6e33-470d-87e6-f7dfcfdebc09"), Name = Enums.Roles.User };
            var adminRole = new Role { Id = Guid.Parse("76695450-ea70-4398-891d-bf1cdfa0d7e4"), Name = Enums.Roles.Admin };
            var supportRole = new Role { Id = Guid.Parse("aef1452f-f830-4c51-91d1-372209eb83d8"), Name = Enums.Roles.Support };
            var superAdminRole = new Role { Id = Guid.Parse("513c041c-bfaa-498d-9433-d66411f24370"), Name = Enums.Roles.SuperAdmin };

            var user1 = new User { Id = Guid.Parse("4cdbb269-bc5a-4d68-85d9-381d8b667df6"), Name = "User 1", Age = 20, Email = "user1@email.com" };
            var user2 = new User { Id = Guid.Parse("d9ebeb68-e7d5-4430-8866-a23660c1a4e2"), Name = "User 2", Age = 36, Email = "user2@email.com" };
            var admin = new User { Id = Guid.Parse("86c1e29a-b899-449d-ac08-7cd798850cd2"), Name = "Admin", Age = 30, Email = "admin@email.com" };
            var support = new User { Id = Guid.Parse("ebe53741-14e6-4cf3-9797-6e3dae227d49"), Name = "Suport", Age = 33, Email = "support@email.com" };
            var superAdmin = new User { Id = Guid.Parse("3d52299a-3ccc-4e11-95a3-c5fd845977ae"), Name = "SuperAdmin", Age = 35, Email = "super.admin@email.com" };
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(u => u.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleUser",
                    r => r.HasOne<Role>().WithMany().HasForeignKey("RolesId"),
                    l => l.HasOne<User>().WithMany().HasForeignKey("UsersId"),
                    je =>
                    {
                        je.HasKey("RolesId", "UsersId");
                        je.HasData(
                            new { RolesId = userRole.Id, UsersId = user1.Id },
                            new { RolesId = userRole.Id, UsersId = user2.Id },
                            new { RolesId = adminRole.Id, UsersId = admin.Id },
                            new { RolesId = supportRole.Id, UsersId = support.Id },
                            new { RolesId = superAdminRole.Id, UsersId = superAdmin.Id },
                            new { RolesId = supportRole.Id, UsersId = superAdmin.Id });
                    });

            modelBuilder.Entity<Role>().HasData(userRole, adminRole, supportRole, superAdminRole);
            modelBuilder.Entity<User>().HasData(user1, user2, admin, support, superAdmin);
        }
    }
}
