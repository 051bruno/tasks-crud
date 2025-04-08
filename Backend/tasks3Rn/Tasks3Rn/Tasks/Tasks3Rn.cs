using MeuProjetoCrudAPI.Data;
using Microsoft.EntityFrameworkCore;
using Tasks3Rn.Models;

namespace Tasks3Rn.Tasks
{
    public class Tasks3Rn
    {
        private readonly AppDbContext _context;

        public Tasks3Rn(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskModel>> GetTasksAsync()
        {
            try
            {
                return await _context.Tasks.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar as tarefas: {ex.Message}");
            }
        }

        public async Task<TaskModel> GetTaskByIdAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }
            return task;
        }

        public async Task CreateTaskAsync(TaskModel taskModel)
        {
            _context.Tasks.Add(taskModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(int id, TaskModel taskModel)
        {
            if (id != taskModel.Id)
            {
                throw new Exception("ID da tarefa não confere.");
            }

            _context.Entry(taskModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
