using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ControleUsuario.Infra.Data.Entities
{
    /// <summary>
    /// Classe de entidade para Usuário
    /// </summary>
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int Codigo { get; set; }
        [Required,StringLength(50)]
        public string Nome { get; set; }
        [Required, StringLength(14)]
        public string Cpf { get; set; }
        [EmailAddress,Required, StringLength(50)]
        public string Login { get; set; }
        [Required, StringLength(50)]
        public string Senha { get; set; }
        public DateTime Ultimo_Acesso { get; set; }
        public int? Qtd_Erro_Login { get; set; }
        public bool? Bl_Ativo { get; set; }
    }
}
