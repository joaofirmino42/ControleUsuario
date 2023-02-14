using ControleUsuario.Infra.Data.Entities;
using ControleUsuario.Infra.Data.Interfaces;
using Dapper;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleUsuario.Infra.Data.Repositories
{
    /// <summary>
    /// Classe para implementar as operações
    /// de banco de dados para Usuário
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        //atributo
        private readonly string _connectionString;
        //método construtor utilizado para que possamos passar
        //o valor da connectionstring para a classe de repositorio
        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Create(Usuario obj)
        {
            var dd = "";
            dd = Convert.ToString(obj.Ultimo_Acesso);
            dd = dd.Substring(0, 10);
            string ddUS = dd.Substring(6, 4) + "/" + dd.Substring(3, 2) + "/" + dd.Substring(0, 2);

            var query = $@"
                 INSERT INTO USUARIO(NOME,LOGIN, CPF,
                SENHA,ULTIMO_ACESSO,QTD_ERRO_LOGIN,BL_ATIVO)
                 VALUES('{obj.Nome}','{obj.Login}','{obj.Cpf}','{obj.Senha}','{ddUS}','{obj.Qtd_Erro_Login}','{obj.Bl_Ativo}')
                 ";
            //conectando no banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                //executando a gravação do evento na base de dados
                connection.Execute(query, obj);
            }

        }

        public void Delete(Usuario obj)
        {

            string sql = $"DELETE FROM USUARIO WHERE USUARIO.CODIGO={obj.Codigo}";
            //conectando no banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                //executando a exclusão do usuário na base de dados
                connection.Execute(sql);
            }
        }

        public List<Usuario> GetAll()
        {
            //Consulta que retorna todos os usuários cadastrados
            List<Usuario> retorno;
            string sql = "SELECT * FROM USUARIO ORDER BY Nome ASC";
            //conectando no banco de dados
            using (var con = new SqlConnection(_connectionString))
            {
                retorno = con.Query<Usuario>(sql).ToList();
            }
            return retorno;
        }

        public Usuario? GetByCpf(string Cpf)
        {
            //Consulta que retorna o usuário pelo cpf
            var query = $@"SELECT * FROM USUARIO WHERE CPF  = '{Cpf}'";
            //conectando no banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                .Query<Usuario>(query)
                .FirstOrDefault();
            }
        }

        public Usuario? GetByEmail(string login)
        {
            //Consulta que retorna o usuário por email 
            var query = $@"SELECT * FROM USUARIO WHERE LOGIN  = '{login}'";
            //conectando no banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                .Query<Usuario>(query)
                .FirstOrDefault();
            }

        }

        public Usuario? GetByEmailESenha(string login, string senha)
        {
            //Consulta que retorna o usuário por email e senha
            var query = $@"SELECT * FROM USUARIO
                   WHERE LOGIN  = '{login}' AND SENHA = '{senha}'";
            //conectando no banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                .Query<Usuario>(query)
                .FirstOrDefault();
            }

        }

        public Usuario GetById(int codigo)
        {
            //Consulta que retorna o usuário pelo código
            Usuario retorno;
            string sql = $"SELECT * FROM USUARIO WHERE USUARIO.CODIGO= {codigo}";
            //conectando no banco de dados
            using (var con = new SqlConnection(_connectionString))
            {
                retorno = con.Query<Usuario>(sql).SingleOrDefault();
            }
            return retorno;
        }

        public void Update(Usuario obj)
        {
            var sql = $@"
                    UPDATE USUARIO SET
                    QTD_ERRO_LOGIN='{obj.Qtd_Erro_Login}',
                      BL_ATIVO='{obj.Bl_Ativo}'  
                      WHERE CODIGO = '{obj.Codigo}'";
            //conectando no banco de dados
            using (var con= new SqlConnection(_connectionString))
            {
                //executando a atualização do evento na base de dados
                con.Execute(sql);
            }
        }
    }
}
