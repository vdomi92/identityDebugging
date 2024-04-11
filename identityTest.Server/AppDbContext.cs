using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace identityTest.Server
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<ContactInfo> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasOne<ContactInfo>(x => x.ContactInfo)
                .WithOne(c => c.User)
                .HasForeignKey<ContactInfo>(c => c.UserStringId);
        }
    }
}
