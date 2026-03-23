using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Parents.Queries.GetParents
{
    public class GetParentsQuery : IRequest<List<Models.ParentDto>>
    {
    }
}
