using Microsoft.AspNetCore.Mvc;

namespace PlayMusic.Controllers
{
    public class PlaylistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
