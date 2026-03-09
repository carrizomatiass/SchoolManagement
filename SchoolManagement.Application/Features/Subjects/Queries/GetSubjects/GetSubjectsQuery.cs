using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Subjects.Queries.GetSubjects
{
    /// <summary>
    /// Query para obtener todas las materias
    /// </summary>
    public class GetSubjectsQuery : IRequest<List<Models.SubjectDto>>
    {
    }
}
