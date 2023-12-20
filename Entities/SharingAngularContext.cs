using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities
{
    public partial class SharingAngularContext : DbContext
    {
        public SharingAngularContext()
        {
        }

        public SharingAngularContext(DbContextOptions<SharingAngularContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<TeamMember> TeamMembers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=SharingAngular;Username=postgres;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Birthdate).HasColumnName("birthdate");

                entity.Property(e => e.Hobby).HasColumnName("hobby");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Opinion).HasColumnName("opinion");

                entity.Property(e => e.Quote).HasColumnName("quote");

                entity.Property(e => e.Role).HasColumnName("role");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("sections");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("teams");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.SectionId).HasColumnName("section_id");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("teams_section_id_fkey");
            });

            modelBuilder.Entity<TeamMember>(entity =>
            {
                entity.ToTable("team_members");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.TeamId).HasColumnName("team_id");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TeamMembers)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("team_members_employee_id_fkey");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamMembers)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("team_members_team_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
