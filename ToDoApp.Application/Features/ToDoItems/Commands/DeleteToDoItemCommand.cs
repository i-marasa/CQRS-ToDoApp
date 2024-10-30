using MediatR;

namespace ToDoApp.Application.Features.ToDoItems.Commands
{
    public class DeleteToDoItemCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
