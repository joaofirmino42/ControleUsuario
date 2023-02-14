using ControleUsuario.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ControleUsuario.Infra.Data.Interfaces
{
    /// <summary>
    /// Interface de repositório para a entidade Usuário
    /// </summary>
    public interface IUsuarioRepository: IBaseRepository<Usuario>
    {
        public Usuario? GetByEmail(string login);
        public Usuario? GetByCpf(string Cpf);
        public Usuario? GetByEmailESenha(string login, string senha);

    }
}
