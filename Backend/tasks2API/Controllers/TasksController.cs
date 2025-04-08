using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tasks3Rn.Models;
using MeuProjetoCrudAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace MeuProjetoCrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly Tasks3Rn.Tasks.Tasks3Rn  _tasks3Rn;

        public TasksController(Tasks3Rn.Tasks.Tasks3Rn tasks3Rn)
        {
            _tasks3Rn = tasks3Rn;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<ret_taskModel>> GetTasks()
        {
            var response = new ret_taskModel();

            try
            {
                response.Data = await _tasks3Rn.GetTasksAsync();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.ErrorMessage = $"Erro ao buscar as tarefas: {ex.Message}";
                return StatusCode(500, response); // Retorna um erro 500 (Internal Server Error)
            }

            return Ok(response);
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetTaskModel(int id)
        {
            try
            {
                var taskModel = await _tasks3Rn.GetTaskByIdAsync(id);
                return Ok(taskModel);
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = true, ErrorMessage = ex.Message });
            }
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskModel(int id, TaskModel taskModel)
        {
            try
            {
                await _tasks3Rn.UpdateTaskAsync(id, taskModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = true, ErrorMessage = ex.Message });
            }
        }

        // POST: api/Tasks
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Tarefa criada com sucesso.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro de parâmetros.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Erro de servidor.")]
        public async Task<ActionResult<TaskModel>> PostTaskModel(TaskModel taskModel)
        {
            if (taskModel == null || string.IsNullOrEmpty(taskModel.NomeTask) || string.IsNullOrEmpty(taskModel.DescricaoTask))
            {
                return BadRequest(new { Error = true, ErrorMessage = "Campos obrigatórios não preenchidos." });
            }

            try
            {
                await _tasks3Rn.CreateTaskAsync(taskModel);
                return CreatedAtAction(nameof(GetTaskModel), new { id = taskModel.Id }, taskModel);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Error = true, ErrorMessage = ex.Message });
            }
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskModel(int id)
        {
            try
            {
                await _tasks3Rn.DeleteTaskAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = true, ErrorMessage = ex.Message });
            }
        }
    }
}
