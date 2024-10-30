using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Features.ToDoItems.Queries
{
    public class GetToDoItemByIdQueryHandler : IRequestHandler<GetToDoItemByIdQuery, ToDoItem>
    {
        private readonly IToDoRepository _repository;

        public GetToDoItemByIdQueryHandler(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ToDoItem> Handle(GetToDoItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
