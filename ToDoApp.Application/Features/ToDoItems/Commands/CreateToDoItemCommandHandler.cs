using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Features.ToDoItems.Commands
{
    public class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, ToDoItem>
    {
        private readonly IToDoRepository _repository;

        public CreateToDoItemCommandHandler(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ToDoItem> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var item = new ToDoItem
            {
                Title = request.Title,
                IsCompleted = false
            };

            return await _repository.AddAsync(item);
        }
    }
}
