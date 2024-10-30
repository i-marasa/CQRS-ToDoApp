using MediatR;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Features.ToDoItems.Commands
{
    public class CreateToDoItemCommand : IRequest<ToDoItem>
    {
        public string Title { get; set; }
    }
}
