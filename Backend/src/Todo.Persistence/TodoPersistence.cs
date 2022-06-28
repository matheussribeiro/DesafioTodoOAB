using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Domain;

namespace Todo.Persistence
{
    public class TodoPersistence : ITodoPersistence
    {       
        private readonly DataContext _context;

        public TodoPersistence(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entity) where T : class
        {
            _context.RemoveRange(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

         public async Task<TodoModel[]> GetAllTodosAsync()
        {
            return await _context.Todos.AsNoTracking().OrderBy(t => t.Id).ToArrayAsync();
        }

        public async Task<TodoModel> GetByIdAsync(int id)
        {
            return await _context.Todos.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}