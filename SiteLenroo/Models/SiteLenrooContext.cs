using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SiteLenroo.Models;

namespace SiteLenroo
{
    public partial class SiteLenrooContext2 : DbContext
    {
        public SiteLenrooContext2()
        {
        }

        public SiteLenrooContext2(DbContextOptions<SiteLenrooContext2> options)
           : base(options)
        {
        }

        public virtual DbSet<AspNetCategory> AspNetCategory { get; set; }
        public virtual DbSet<AspNetNews> AspNetNews { get; set; }
        public virtual DbSet<AspNetPhoto> AspNetPhoto { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetTag> AspNetTag { get; set; }
        public virtual DbSet<AspNetTree> AspNetTree { get; set; }
        public virtual DbSet<AspNetTreeMain> AspNetTreeMain { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=PROGRAMMER;Database=SiteLenroo;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetCategory>(entity =>
            {
                entity.Property(e => e.Category).IsRequired();
            });

            modelBuilder.Entity<AspNetNews>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.PreviewText).IsRequired();

                entity.Property(e => e.Title).IsRequired();

                entity.Property(e => e.Url).IsRequired();

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.AspNetNews)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("FK_AspNetNews_AspNetCategory");

                entity.HasOne(d => d.PreviewPhotoNavigation)
                    .WithMany(p => p.AspNetNews)
                    .HasForeignKey(d => d.PreviewPhoto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AspNetNews_AspNetPhoto");

                entity.HasOne(d => d.TagNavigation)
                    .WithMany(p => p.AspNetNews)
                    .HasForeignKey(d => d.Tag)
                    .HasConstraintName("FK_AspNetNews_AspNetTag");
            });

            modelBuilder.Entity<AspNetPhoto>(entity =>
            {
                entity.Property(e => e.DateAdd).HasColumnType("datetime");

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetTag>(entity =>
            {
                entity.Property(e => e.Tag).IsRequired();
            });

            modelBuilder.Entity<AspNetTree>(entity =>
            {
                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.ParentMainNavigation)
                    .WithMany(p => p.AspNetTree)
                    .HasForeignKey(d => d.ParentMain)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AspNetTree_AspNetTreeMain");
            });

            modelBuilder.Entity<AspNetTreeMain>(entity =>
            {
                entity.Property(e => e.Title).IsRequired();

                entity.Property(e => e.Url).IsRequired();
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });
        }
    }
}
