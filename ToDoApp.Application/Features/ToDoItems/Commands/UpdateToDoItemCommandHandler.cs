using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Features.ToDoItems.Commands
{
    public class UpdateToDoItemCommandHandler : IRequestHandler<UpdateToDoItemCommand, ToDoItem>
    {
        private readonly IToDoRepository _repository;

        public UpdateToDoItemCommandHandler(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ToDoItem> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(request.Id);
            if (item == null)
            {
                throw new System.Exception("ToDoItem not found");
            }

            item.Title = request.Title;
            item.IsCompleted = request.IsCompleted;

            await _repository.UpdateAsync(item);
            return item;
        }
    }
}
