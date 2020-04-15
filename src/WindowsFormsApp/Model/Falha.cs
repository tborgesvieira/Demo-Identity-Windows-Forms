using System.Collections.Generic;

namespace WindowsFormsApp.Model
{
    public class Erro
    {
        public string MensageErro { get; set; }
    }

    public class Falha
    {
        public bool Success { get; set; }
        public List<Erro> Erros { get; set; }
    }
}
