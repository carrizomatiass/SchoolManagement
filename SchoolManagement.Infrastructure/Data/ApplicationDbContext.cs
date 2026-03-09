using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Users and Profiles
        public DbSet<User> Users => Set<User>();
        public DbSet<Profile> Profiles => Set<Profile>();

        // Roles específicos
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Parent> Parents => Set<Parent>();
        public DbSet<StudentParent> StudentParents => Set<StudentParent>();

        // Estructura académica
        public DbSet<AcademicYear> AcademicYears => Set<AcademicYear>();
        public DbSet<Term> Terms => Set<Term>();
        public DbSet<Grade> Grades => Set<Grade>();
        public DbSet<Section> Sections => Set<Section>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<CourseSubject> CourseSubjects => Set<CourseSubject>();

        // Asignaciones y matrículas
        public DbSet<TeacherAssignment> TeacherAssignments => Set<TeacherAssignment>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        // Calificaciones y asistencias
        public DbSet<GradeRecord> GradeRecords => Set<GradeRecord>();
        public DbSet<Attendance> Attendances => Set<Attendance>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar todas las configuraciones
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Configurar eliminación en cascada según necesidad
            var foreignKeys = modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys());

            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Domain.Common.BaseEntity &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entity = (Domain.Common.BaseEntity)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }
                else
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
