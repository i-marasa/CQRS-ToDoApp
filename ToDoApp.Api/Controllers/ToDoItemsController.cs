using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Application.Features.ToDoItems.Commands;
using ToDoApp.Application.Features.ToDoItems.Queries;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToDoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/ToDoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            var query = new GetAllToDoItemsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(int id)
        {
            var query = new GetToDoItemByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/ToDoItems
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> CreateToDoItem(CreateToDoItemCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetToDoItem), new { id = result.Id }, result);
        }

        // PUT: api/ToDoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDoItem(int id, UpdateToDoItemCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            try
            {
                await _mediator.Send(command);
            }
            catch (System.Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/ToDoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            var command = new DeleteToDoItemCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
