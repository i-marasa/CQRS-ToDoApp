using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Interfaces
{
    public interface IToDoRepository
    {
        Task<IEnumerable<ToDoItem>> GetAllAsync();
        Task<ToDoItem> GetByIdAsync(int id);
        Task<ToDoItem> AddAsync(ToDoItem item);
        Task UpdateAsync(ToDoItem item);
        Task DeleteAsync(int id);
    }
}
