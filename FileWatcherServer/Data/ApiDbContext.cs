using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileWatcherServer.Data
{
    public class ApiDbContext : IdentityDbContext<IdentityUser>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> opt) : base(opt)
        {

        }

        public DbSet<IdentityUser> AppUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
              new { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
              new { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            IdentityUser boldi = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "boldi333333@gmail.com",
                EmailConfirmed = true,
                UserName = "boldi333333@gmail.com",
                NormalizedUserName = "BOLDI333333@GMAIL.COM"
            };
            boldi.PasswordHash = ph.HashPassword(boldi, "jelszo123");
            builder.Entity<IdentityUser>().HasData(boldi);

            base.OnModelCreating(builder);
        }
    }
}
