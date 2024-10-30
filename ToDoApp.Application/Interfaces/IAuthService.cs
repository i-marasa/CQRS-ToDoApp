using System.Threading.Tasks;
using ToDoApp.Application.Models;

namespace ToDoApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterModel model);
        Task<AuthResponse> LoginAsync(LoginModel model);
    }
}
