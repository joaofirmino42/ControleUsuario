using Microsoft.AspNetCore.Mvc;
using ControleUsuario.Presentation.Models;
using ControleUsuario.Infra.Data.Entities;
using ControleUsuario.Infra.Data.Interfaces;
using ControleUsuario.Infra.Data.Utils;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ControleUsuario.Presentation.Controllers
{
    public class AccountController : Controller
    {
        //atributo
        private readonly IUsuarioRepository _usuarioRepository;
        //construtor para inicializar o atributo
        public AccountController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CadastroUsuario()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CadastrarUsuario(UsuarioCadastroViewModel model)
        {
            try
            {
                if (!CpfUtil.IsCpf(model.Cpf))
                {
                    return Json(new { success = false, message = "Cpf incorreto" });
                }
                if (model.Senha.Length <= 6 && model.SenhaConfirmacao.Length <= 6)
                {
                    return Json(new { success = false, message = "Senha curta! Por favor escreva pelos menos 7 caracteres" });
                }
                if (_usuarioRepository.GetByEmail(model.Login) != null)
                {
                    return Json(new { success = false, message = "Login já cadastrado" });
                }
                if (_usuarioRepository.GetByCpf(model.Cpf) != null)
                {
                    return Json(new { success = false, message = "Cpf já cadastrado" });
                }
                Usuario usuario = new Usuario()
                {

                    Bl_Ativo = true,
                    Login = model.Login,
                    Senha = CriptografiaUtil.CreateMD5(model.Senha),
                    Nome = model.Nome,
                    Cpf = model.Cpf,
                    Qtd_Erro_Login = 0,
                    Ultimo_Acesso = DateTime.Now

                };

                _usuarioRepository.Create(usuario);
                return Json(new { success = true, message = "Usuário cadastrado com sucesso" });
            }
            catch (Exception e)
            {

                return Json(new { success = false, message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult Login(string email, string senha, int contadorErros)
        {
            int cont = 0;

            try
            {
                //procurar o usuário no banco de dados
                //através do email e senha
                var usuario = _usuarioRepository.GetByEmailESenha(email, CriptografiaUtil.CreateMD5(senha));
                //verificar se o usuário foi encontrado
                if (usuario != null && contadorErros >= 3)
                {
                    Usuario usuarioInativo = new Usuario
                    {
                        Codigo = usuario.Codigo,
                        Qtd_Erro_Login = contadorErros,
                        Bl_Ativo = false
                    };
                    _usuarioRepository.Update(usuarioInativo);
                }


                if (usuario != null)
                {
                    if ((bool)usuario.Bl_Ativo)
                    {


                        //converter o objeto em json
                        UserIdentityModel user = new UserIdentityModel()
                        {
                            Codigo = usuario.Codigo,
                            Cpf = usuario.Cpf,
                            Nome = usuario.Nome,
                            Login = usuario.Login,
                            Ultimo_Acesso = usuario.Ultimo_Acesso
                        };
                        var json = JsonConvert.SerializeObject
                         (user);
                        HttpContext.Session.SetString("usuario", json);
                        TempData["MensagemSucesso"] = $"Parabéns, !Acesso ao sistema realizado com sucesso.";
                        #region Criando a permissão de acesso do usuário
                        var autorizacao = new ClaimsIdentity(new[] { new
Claim(ClaimTypes.Name, usuario.Codigo.ToString()) },
                        CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimPrincipal = new
                       ClaimsPrincipal(autorizacao);
                        HttpContext.SignInAsync
                       (CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);
                        #endregion

                        //redirecionar para a página inicial do projeto
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        cont = contadorErros + 1;
                        ViewBag.cont = cont;
                        if (contadorErros > 3)
                        {
                            TempData["MensagemErro"]
                     = "Usuário Inativo.";

                        }
                    }

                }
                else
                {


                    TempData["MensagemErro"]
                = "Acesso negado. Usuário inválido.";


                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View("Index");
        }

        public async Task<IActionResult> Logout()
        {
            //apagar os dados do usuário da sessão
            HttpContext.Session.Remove("usuario");
            //apagar a permissão dada ao usuário autenticado
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Account");
        }

    }

}
