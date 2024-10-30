using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Features.ToDoItems.Commands
{
    public class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand, bool>
    {
        private readonly IToDoRepository _repository;

        public DeleteToDoItemCommandHandler(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
            return true;
        }
    }
}
