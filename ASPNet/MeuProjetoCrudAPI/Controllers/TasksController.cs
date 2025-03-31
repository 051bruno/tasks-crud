using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeuProjetoCrudAPI.Data;
using MeuProjetoCrudAPI.Models;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;

namespace MeuProjetoCrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<ret_taskModel>> GetTasks()
        {
            var response = new ret_taskModel();

            try
            {
                response.Data = await _context.Tasks.ToListAsync();
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
            var taskModel = await _context.Tasks.FindAsync(id);

            if (taskModel == null)
            {
                return NotFound();
            }

            return taskModel;
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskModel(int id, TaskModel taskModel)
        {
            if (id != taskModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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
                // Criação da tarefa
                _context.Tasks.Add(taskModel);
                await _context.SaveChangesAsync();

                // Retorno com a tarefa criada
                return CreatedAtAction(nameof(GetTaskModel), new { id = taskModel.Id }, taskModel);
            }
            catch (DbUpdateException)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Error = true, ErrorMessage = "Erro ao salvar a tarefa no banco de dados." });
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
            var taskModel = await _context.Tasks.FindAsync(id);
            if (taskModel == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(taskModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskModelExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
