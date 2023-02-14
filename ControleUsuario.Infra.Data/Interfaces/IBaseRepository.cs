using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleUsuario.Infra.Data.Interfaces
{
    /// <summary>
    /// Interface genérica para as operações do repositório
    /// </summary>
    public interface IBaseRepository<TEntity>
  where TEntity : class
    {
        void Create(TEntity obj);
        void Update(TEntity obj);
        void Delete(TEntity obj);
        List<TEntity> GetAll();
        TEntity GetById(int codigo);
    }
}
