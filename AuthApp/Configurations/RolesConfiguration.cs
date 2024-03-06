using AuthApp.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthApp.Configurations {
    public class RolesConfiguration : IEntityTypeConfiguration<IdentityRole> {
        public void Configure(EntityTypeBuilder<IdentityRole> modelBuilder) {
            List<IdentityRole> roles = new List<IdentityRole> {
                new IdentityRole {
                    Name = UserRoles.Admin,
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
                new IdentityRole {
                    Name = UserRoles.User,
                    NormalizedName = UserRoles.User.ToUpper()
                }
            };
            modelBuilder.HasData(roles);
        }
    }
}
