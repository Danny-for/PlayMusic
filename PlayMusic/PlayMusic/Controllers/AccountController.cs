using Microsoft.AspNetCore.Mvc;

namespace PlayMusic.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        public IActionResult VerificandoEmail()
        {
            return View();
        }

        public IActionResult AlterarSenha()
        {
            return View();
        }
    }
}
