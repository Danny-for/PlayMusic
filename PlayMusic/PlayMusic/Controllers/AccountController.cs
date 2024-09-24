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

        [HttpPost]
        public async Task<IActionResult> VerificandoEmail(VerificandoEmailViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var usuario = await userManager.FindByNameAsync(model.Email);

                if (usuario == null)
                {
                    ModelState.AddModelError("", "Usuário não encontrado.");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("AlterarSenha", "Account", new {username = usuario.UserName});
                }
            }
            return View(model);
        }

        public IActionResult AlterarSenha(string username)
        {
            if(string.IsNullOrEmpty(username))
            {
                return RedirectToAction("verificandoEmail", "Account");
            }
            return View(new AlterarSenhaViewModel { Email = username});
        }

        [HttpPost]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await userManager.FindByNameAsync(model.Email);
                if (usuario != null)
                {
                    var resultado = await userManager.RemovePasswordAsync(usuario);
                    if (resultado.Succeeded)
                    {
                        resultado = await userManager.AddPasswordAsync(usuario, model.NewPassword);
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {

                        foreach (var error in resultado.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email não encontrado!");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong. try again.");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
