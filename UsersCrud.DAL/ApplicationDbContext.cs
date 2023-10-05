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
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(u => u.Users);

            modelBuilder.Entity<Role>()
                .HasData(
                    new Role { Id = Guid.Parse("2dc2c800-6e33-470d-87e6-f7dfcfdebc09"), Name = Enums.Roles.User },
                    new Role { Id = Guid.Parse("76695450-ea70-4398-891d-bf1cdfa0d7e4"), Name = Enums.Roles.Admin },
                    new Role { Id = Guid.Parse("aef1452f-f830-4c51-91d1-372209eb83d8"), Name = Enums.Roles.Support },
                    new Role { Id = Guid.Parse("513c041c-bfaa-498d-9433-d66411f24370"), Name = Enums.Roles.SuperAdmin });
        }
    }
}
