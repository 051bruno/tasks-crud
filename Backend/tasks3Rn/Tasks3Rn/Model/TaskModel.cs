using System.ComponentModel.DataAnnotations;

namespace Tasks3Rn.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string NomeTask { get; set; }
        public string DescricaoTask {  get; set; }
        public bool isTaskConcluida { get; set; }
    }
}
