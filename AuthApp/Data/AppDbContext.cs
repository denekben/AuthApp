using AuthApp.Configurations;
using AuthApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthApp.Data {
    public class AppDbContext : IdentityDbContext<AppUser> {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
