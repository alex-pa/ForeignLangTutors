using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ForeignLangTutors.Models
{
    public partial class ForeignLangTutorsDBContext : DbContext
    {
        public ForeignLangTutorsDBContext()
        {
        }

        public ForeignLangTutorsDBContext(DbContextOptions<ForeignLangTutorsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Classes> Classes { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<GroupsStudents> GroupsStudents { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Tutors> Tutors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classes>(entity =>
            {
                entity.Property(e => e.BeginningTime).HasColumnType("datetime");

                entity.Property(e => e.Topic)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.СompletionTime).HasColumnType("datetime");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Classes_ToTable");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Classes_ToTable1");
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.TutorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Groups_ToTable");
            });

            modelBuilder.Entity<GroupsStudents>(entity =>
            {
                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupsStudents)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupsStudents_ToTable1");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.GroupsStudents)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupsStudents_ToTable");
            });

            modelBuilder.Entity<Rooms>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.Property(e => e.Adress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tutors>(entity =>
            {
                entity.Property(e => e.Adress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
