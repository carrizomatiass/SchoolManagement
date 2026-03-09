using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Profile> Profiles { get; }
    DbSet<Teacher> Teachers { get; }
    DbSet<Student> Students { get; }
    DbSet<Parent> Parents { get; }
    DbSet<StudentParent> StudentParents { get; }
    DbSet<AcademicYear> AcademicYears { get; }
    DbSet<Term> Terms { get; }
    DbSet<Grade> Grades { get; }
    DbSet<Section> Sections { get; }
    DbSet<Course> Courses { get; }
    DbSet<Subject> Subjects { get; }
    DbSet<CourseSubject> CourseSubjects { get; }
    DbSet<TeacherAssignment> TeacherAssignments { get; }
    DbSet<Enrollment> Enrollments { get; }
    DbSet<GradeRecord> GradeRecords { get; }
    DbSet<Attendance> Attendances { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}