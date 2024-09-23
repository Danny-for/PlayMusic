using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlayMusic.Models;
using PlayMusic.ViewModels;

namespace PlayMusic.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Usuario> signInManager;
        private readonly UserManager<Usuario> userManager;

        public AccountController(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var resultado = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);


                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email ou senha está incorreto.");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel model)
        {
            if (ModelState.IsValid) 
            {
                Usuario usuarios = new Usuario
                {
                    Nome = model.Nome,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var resultado = await userManager.CreateAsync(usuarios, model.Password);

                if(resultado.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach(var error in resultado.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
            }
            return View(model);
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
