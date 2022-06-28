using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Interface;
using Todo.Domain;
using Todo.Persistence;

namespace Todo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var todos = await _todoService.GetAllTodos();
                if(todos == null) return NotFound("Nenhuma Tarefa encontrada");

                return Ok(todos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao buscar todas as tarefas, {ex.Message}");
            }
        }

        [HttpGet("pendingtasks")]
        public async Task<IActionResult> GetAllPendingTasks()
        {
            try
            {
                var todos = await _todoService.GetAllTodos();
                var pendingTodo = todos.Where(t => t.IsComplete == false);
                if(pendingTodo == null) return NotFound("Nenhuma Tarefa encontrada");

                return Ok(pendingTodo);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao buscar todas as tarefas, {ex.Message}");
            }
        }

        [HttpGet("completetasks")]
        public async Task<IActionResult> GetAllCompleteTasks()
        {
            try
            {
                var todos = await _todoService.GetAllTodos();
                var completeTodos = todos.Where(t => t.IsComplete == true);
                if(completeTodos == null) return NotFound("Nenhuma Tarefa encontrada");

                return Ok(completeTodos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao buscar todas as tarefas, {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var todo = await _todoService.GetTodoById(id);
                if(todo == null) return NotFound("Tarefa não encontrada");
                return Ok(todo);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao encontrar a tarefa, {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoRequest request)
        {
            try
            {
                var todo = new TodoModel
                {
                Name = request.Name,
                Description = request.Description,
                IsComplete = false,
                DtCreated = DateTime.Now
                };

                todo = await _todoService.AddTodo(todo);
                if(todo == null) return BadRequest("Erro ao criar tarefa");

                return Ok(todo);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao criar tarefa, {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TodoModel todo)
        {

            try
            {
                var todoUpdate = await _todoService.UpdateTodo(id, todo);
                if(todoUpdate == null) return NotFound("Tarefa não encontrada");

                return Ok(todoUpdate);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao atualizar a  tarefa, {ex.Message}");
            }
        
        }
        //teste
        [HttpPut("updatestatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id,[FromBody] bool status)
        {

            try
            {
                var todoStatusUpdate = await _todoService.UpdateStatus(id,status);
                return Ok(todoStatusUpdate);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao atualizar a  tarefa, {ex.Message}");
            }
        
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _todoService.DeleteTodo(id)?
                    Ok("Tarefa deletada") :
                    BadRequest("Erro ao deletar tarefa");
            }
             catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao deletar a  tarefa, {ex.Message}");
            }
        } 
    }
}
