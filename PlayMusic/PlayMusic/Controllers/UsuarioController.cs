using Microsoft.AspNetCore.Mvc;

namespace PlayMusic.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
