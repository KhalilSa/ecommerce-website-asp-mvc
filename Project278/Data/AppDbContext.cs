using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project278.Models;

namespace Project278.Data
{
    public class AppDbContext: IdentityDbContext<User>
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Label> Labels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seeding Users and roles
            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<User>();


            // Adding a Role
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { 
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", 
                    Name = "Admin", 
                    NormalizedName = "ADMIN".ToUpper() 
                });

            //Seeding the User to AspNetUsers table
            builder.Entity<User>().HasData(
                new User
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                    UserName = "Abe",
                    AvatarUrl = "https://www.w3schools.com/howto/img_avatar2.png",
                    Gender = "Male",
                    Age = 19,
                    Email = "abe@domain.com",
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd"),
                }
            );

            builder.Entity<IdentityUserRole<String>>().HasData(
                new IdentityUserRole<String>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            );

            // Configure One to many relationship between artist and labels
            builder.Entity<Label>()
                .HasOne(l => l.Artist)
                .WithMany(a => a.Labels)
                .HasForeignKey(l => l.ArtistId);

            // Configure Many to many relationship between artists and bands
            builder.Entity<ArtistBand>()
                .HasKey(k => new { k.ArtistId, k.BandId });

            builder.Entity<ArtistBand>()
                .HasOne(ab => ab.Artist)
                .WithMany(a => a.ArtistBands)
                .HasForeignKey(ab => ab.ArtistId);

            builder.Entity<ArtistBand>()
                .HasOne(ab => ab.Band)
                .WithMany(b => b.ArtistBands)
                .HasForeignKey(ab => ab.BandId);
        }
    }
}
