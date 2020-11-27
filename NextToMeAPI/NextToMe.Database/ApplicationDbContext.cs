using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NextToMe.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextToMe.Database
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        private static bool _checkedForMigrations;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            if (!_checkedForMigrations)
            {
                _checkedForMigrations = true;
                try
                {
                    this.Database.Migrate();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<MessageComment> MessageComments { get; set; }

        public virtual DbSet<UserImage> UserImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
               .HasMany(x => x.RefreshTokens)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<User>()
               .HasMany(x => x.Messages)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.MessageComments)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.UserImage)
                .WithOne(x => x.User)
                .HasForeignKey<UserImage>(x => x.UserId);

            modelBuilder.Entity<Message>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Message)
                .HasForeignKey(x => x.MessageId);
        }

    }
}
