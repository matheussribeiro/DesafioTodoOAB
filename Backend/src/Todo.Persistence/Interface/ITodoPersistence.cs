using System.Threading.Tasks;
using Todo.Domain;

namespace Todo.Persistence
{
    public interface ITodoPersistence
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entity) where T : class;
        Task<bool> SaveChangesAsync();

        //GETS

        Task<TodoModel> GetByIdAsync(int id);
        Task<TodoModel[]> GetAllTodosAsync();

    }
}