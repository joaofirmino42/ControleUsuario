namespace ControleUsuario.Presentation.Models
{
    /// <summary>
    /// Classe model para autenticar o Usuário logado
    /// </summary>
    public class UserIdentityModel
    {
        public int Codigo { get; set; }
        public string? Nome { get; set; }
       
        public string? Login { get; set; }
        public string? Cpf { get; set; }
        public DateTime Ultimo_Acesso { get; set; }

    }
}
