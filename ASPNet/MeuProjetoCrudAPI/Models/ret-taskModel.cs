using System.Runtime.Serialization;

namespace MeuProjetoCrudAPI.Models
{
    [DataContract]
    public class ret_taskModel : ReturnModel<List<TaskModel>>
    {
        [DataMember(Name = "Data")]
        public override List<TaskModel> Data { get; set; }

        public ret_taskModel()
        {
            this.Error = false;
            this.ErrorMessage = "";
            this.Data = new List<TaskModel>();
        }
        
    }
}
