using System.Threading.Tasks;
using Todo.Domain;

namespace Todo.Application.Interface
{
    public interface ITodoService
    {
        Task<TodoModel> AddTodo(TodoModel todo);
        Task<TodoModel> UpdateTodo(int id, TodoModel todo);
        Task<bool> DeleteTodo(int id);
        Task<TodoModel> GetTodoById(int id);
        Task<TodoModel[]> GetAllTodos();
        Task<bool> UpdateStatus(int id, bool status);
         
    }
}