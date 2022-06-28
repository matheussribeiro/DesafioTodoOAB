using System;
using System.Threading.Tasks;
using Todo.Application.Interface;
using Todo.Domain;
using Todo.Persistence;

namespace Todo.Application
{
    public class TodoService : ITodoService
    {
        private readonly ITodoPersistence _todoPersistence;
        public TodoService(ITodoPersistence todoPersistence)
        {
            _todoPersistence = todoPersistence;
        }

        public async Task<TodoModel> AddTodo(TodoModel todo)
        {
            try
            {
                _todoPersistence.Add(todo);
                if(await _todoPersistence.SaveChangesAsync())
                {
                    return todo;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<bool> DeleteTodo(int id)
        {
            try
            {
                var todo = await _todoPersistence.GetByIdAsync(id);
                if(todo == null) throw new Exception("Todo não encontrado");

                _todoPersistence.Delete(todo);
                return await _todoPersistence.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<TodoModel[]> GetAllTodos()
        {
            try
            {
                var todos = await _todoPersistence.GetAllTodosAsync();
                if(todos == null ) return null;

                return todos;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<TodoModel> GetTodoById(int id)
        {
            try
            {
                var todo = await _todoPersistence.GetByIdAsync(id);
                if(todo == null ) return null;

                return todo;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateStatus(int id, bool status)
        {
            var todo = await _todoPersistence.GetByIdAsync(id);
            todo.IsComplete = status;
            _todoPersistence.Update(todo);
            if(await _todoPersistence.SaveChangesAsync())
                {
                    return todo.IsComplete;
                }
                throw new Exception("Erro ao atualizar o Status");
        }

        public async Task<TodoModel> UpdateTodo(int id, TodoModel todo)
        {
            try
            {
                var getTodo = await _todoPersistence.GetByIdAsync(id);
                if(getTodo == null) return null;

                todo.Id = getTodo.Id;
                todo.DtCreated = getTodo.DtCreated;

                _todoPersistence.Update(todo);
                if(await _todoPersistence.SaveChangesAsync())
                {
                    return todo;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}