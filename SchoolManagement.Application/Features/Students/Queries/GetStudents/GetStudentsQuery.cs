using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Students.Queries.GetStudents
{
    public class GetStudentsQuery : IRequest<List<Models.StudentDto>>
    {
        public bool? OnlyActive { get; set; }
        public Guid? CourseId { get; set; }
    }
}
