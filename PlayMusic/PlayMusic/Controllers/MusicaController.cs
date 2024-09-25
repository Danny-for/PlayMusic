using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayMusic.Data;
using PlayMusic.Models;

namespace PlayMusic.Controllers
{
    public class MusicaController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<Usuario> _userManager;


        public MusicaController(UserManager<Usuario> userManager, DataContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public IActionResult Adicionar(int playlistId)
        {
            ViewBag.PlaylistId = playlistId; 
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Adicionar(Musica musica)
        {
            if (ModelState.IsValid)
            {
                // Atribui o PlaylistId recebido do formulário
                _context.Musicas.Add(musica);
                await _context.SaveChangesAsync();
                return RedirectToAction("Visualizar", "Musica", new { playlistId = musica.PlaylistId });
            }

            return View(musica);
        }


        public async Task<IActionResult> Visualizar(int playlistId)
        {
            var musicas = await _context.Musicas.Where(m => m.PlaylistId == playlistId).ToListAsync();
            ViewBag.PlaylistId = playlistId; // Passa o ID da playlist para a view
            return View(musicas);
        }


    }

}
