namespace MeuProjetoCrudAPI.Models
{
    public class ReturnModel<T>
    {
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
        public virtual T Data { get; set; } // Propriedade genérica para armazenar os dados retornados

        public ReturnModel()
        {
            Error = false;
            ErrorMessage = string.Empty;
            Data = default; // Pode ser null ou um valor padrão do tipo T
        }
    }

}
