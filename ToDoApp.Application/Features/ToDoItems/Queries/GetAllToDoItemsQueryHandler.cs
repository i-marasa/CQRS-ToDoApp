using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Features.ToDoItems.Queries
{
    public class GetAllToDoItemsQueryHandler : IRequestHandler<GetAllToDoItemsQuery, IEnumerable<ToDoItem>>
    {
        private readonly IToDoRepository _repository;

        public GetAllToDoItemsQueryHandler(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ToDoItem>> Handle(GetAllToDoItemsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
