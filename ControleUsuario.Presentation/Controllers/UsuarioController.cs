using ControleUsuario.Infra.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleUsuario.Presentation.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        //atributo
        private readonly IUsuarioRepository _usuarioRepository;
        //construtor para inicializar o atributo
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult ConsultaUsuarios()
        {
            var listaUsuarios = _usuarioRepository.GetAll();
            return View(listaUsuarios);
        }
    }
}
