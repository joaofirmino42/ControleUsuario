using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleUsuario.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
           
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
