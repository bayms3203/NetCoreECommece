using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TestMVCApp.libs.Data.Contexts
{
    public class ApplicationIdentityDbContext:  IdentityDbContext<IdentityUser,IdentityRole, string>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
           builder.Entity<IdentityUser>().ToTable("User"); // AspNetUser
           builder.Entity<IdentityRole>().ToTable("Role"); // AspNetRole
           builder.Entity<IdentityUserRole<string>>().ToTable("UserRole"); // AspNetUserRole
           builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim"); // AspNetUserClaim 
           // user ait özgü bilgilerin key value cinsinden saklandığı tablo
           // twitter adresi twitter_url= www.twitter/a.p?100
           // age 21
           // nation TR
           builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim"); // role özgü özellikler
           // moderatör rolunun 2 farklı özelliği varmış
           // shared_key = 143214234
           // group => public
           builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin"); // userLogin , sisteme yapılan her bir login kaydı history olarak tutulur.
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken"); // VerifyToken, RegisterationToken, PhoneVaalidationToken vs




            base.OnModelCreating(builder);
        }





     
    }
}