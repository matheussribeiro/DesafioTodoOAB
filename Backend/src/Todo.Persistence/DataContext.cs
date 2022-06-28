using Microsoft.EntityFrameworkCore;
using Todo.Domain;

namespace Todo.Persistence
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<TodoModel> Todos { get; set; }
        
    }
}