using Di2P1G3.Authentication.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Di2P1G3.Authentication.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AccessToken> AccessTokens { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<ClientApplication> ClientApplications { get; set; }

        public DbSet<User> Users { get; set; }

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<User>().HasIndex(user => user.Username).IsUnique();
            modelBuilder.Entity<AccessToken>().HasKey(accessToken => accessToken.Id);
            modelBuilder.Entity<RefreshToken>().HasKey(refreshToken => refreshToken.Id);
            modelBuilder.Entity<ClientApplication>().HasKey(clientApplication => clientApplication.Id);

            modelBuilder.Entity<AccessToken>().HasOne(token => token.User).WithMany(user => user.Tokens)
                .HasForeignKey(token => token.IdUser);
            modelBuilder.Entity<RefreshToken>().HasOne(token => token.AccessToken)
                .WithMany(token => token.RefreshTokens)
                .HasForeignKey(token => token.IdAccessToken);

            modelBuilder.Entity<ClientApplication>().HasMany(application => application.Users)
                .WithMany(user => user.Applications);
            modelBuilder.Entity<ClientApplication>().HasIndex(application => application.Name).IsUnique();


            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=51.103.30.136,1433;Database=Di2P1G3;User Id=sa;Password=OfficeSQL2019.;Trusted_Connection=False;MultipleActiveResultSets=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}