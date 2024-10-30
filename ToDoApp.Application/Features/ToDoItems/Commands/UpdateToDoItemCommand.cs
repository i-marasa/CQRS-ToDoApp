using MediatR;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Features.ToDoItems.Commands
{
    public class UpdateToDoItemCommand : IRequest<ToDoItem>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
