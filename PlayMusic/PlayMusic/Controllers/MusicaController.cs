using Microsoft.AspNetCore.Mvc;

namespace PlayMusic.Controllers
{
    public class MusicaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
