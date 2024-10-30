using MediatR;
using System.Collections.Generic;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Features.ToDoItems.Queries
{
    public class GetAllToDoItemsQuery : IRequest<IEnumerable<ToDoItem>>
    {
    }
}
