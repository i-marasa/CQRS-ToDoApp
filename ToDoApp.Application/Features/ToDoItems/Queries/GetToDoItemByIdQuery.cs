using MediatR;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Features.ToDoItems.Queries
{
    public class GetToDoItemByIdQuery : IRequest<ToDoItem>
    {
        public int Id { get; set; }
    }
}
