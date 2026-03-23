using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Parents.Commands.AssignParentToStudent;
using SchoolManagement.Application.Features.Parents.Commands.CreateParent;
using SchoolManagement.Application.Features.Parents.Queries.GetParents;

namespace SchoolManagement.API.Controllers
{
    // <summary>
    /// Controlador para gestionar padres/tutores
    /// Permite crear padres y asociarlos con alumnos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ParentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetParentsQuery());
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Secretary")]
        public async Task<IActionResult> Create([FromBody] CreateParentCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { id, message = "Padre/tutor creado exitosamente" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("assign-to-student")]
        [Authorize(Roles = "Admin,Secretary")]
        public async Task<IActionResult> AssignToStudent([FromBody] AssignParentToStudentCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { id, message = "Padre/tutor asociado al alumno exitosamente" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
