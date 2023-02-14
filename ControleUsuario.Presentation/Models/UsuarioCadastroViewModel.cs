using System.ComponentModel.DataAnnotations;
namespace ControleUsuario.Presentation.Models
{
    /// <summary>
    /// Classe model para cadastro de Usuário
    /// </summary>
    public class UsuarioCadastroViewModel
    {
        [Required(ErrorMessage ="Por favor, informe o nome do usuário")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Por favor, informe o email do usuário"), EmailAddress]
        public string Login { get; set; }
        [Required(ErrorMessage = "Por favor, informe a senha do usuário")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Por favor, informe a senha de confirmação do usuário")]
        public string SenhaConfirmacao { get; set; }
        [Required(ErrorMessage = "Por favor, informe o Cpf do usuário")]
        public string Cpf { get; set; }
    }
}
