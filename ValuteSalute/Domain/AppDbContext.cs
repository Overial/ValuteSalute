using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ValuteSalute.Domain.Entities;

namespace ValuteSalute.Domain
{
    public class AppDbContext
        : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            // Database.EnsureCreated();
        }

        public DbSet<TextField>? TextFields { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var AdminRoleGuid = Guid.NewGuid().ToString();
            var AdminUserGuid = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = AdminRoleGuid,
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = AdminUserGuid,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.ru",
                NormalizedEmail = "ADMIN@EXAMPLE.RU",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "admin"),
                SecurityStamp = String.Empty
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = AdminRoleGuid,
                UserId = AdminUserGuid,
            });

            builder.Entity<TextField>().HasData(new TextField
            {
                Id = Guid.NewGuid(),
                CodeWord = "PageIndex",
                Title = "Главная",
                Text = "<p>Lorem ipsum!</p><p>Содержание главной страницы</p>",
                MetaTitle = "VS – Главная"
            });

            builder.Entity<TextField>().HasData(new TextField
            {
                Id = Guid.NewGuid(),
                CodeWord = "PageContacts",
                Title = "Контакты",
                Text = "<p>Список контактов:</p><p>Афанасьев Вадим | Программист C# | valutesalute@example.ru | +7 (123) 456-78-90 </p>",
                MetaTitle = "VS – Контакты"
            });
        }
    }
}
