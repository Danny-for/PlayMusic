using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayMusic.Data;
using PlayMusic.Models;
using PlayMusic.ViewModels;

namespace PlayMusic.Controllers
{
    
    public class PlaylistController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<Usuario> _userManager;


        public PlaylistController(UserManager<Usuario> userManager, DataContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var playlists = await _context.Playlists.ToListAsync();
            return View(playlists);
        }


        [Authorize]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Criar(Playlist model)
        {
            // Remove as chaves que estão causando os erros de validação
            ModelState.Remove("Usuario");
            ModelState.Remove("UsuarioId");

            if (!ModelState.IsValid)
            {
                // Exibi erros do ModelState no console
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Key: {state.Key}, Error: {error.ErrorMessage}");
                    }
                }
                return View(model);
            }

          
            var user = await _userManager.GetUserAsync(User);
            model.UsuarioId = user.Id;
            model.Usuario = user;

          
            _context.Playlists.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }

            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); 
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
                _context.Musicas.Add(musica);
                await _context.SaveChangesAsync();
                return RedirectToAction("Visualizar", "Musica", new { playlistId = musica.PlaylistId });
            }
            return View(musica); 
        }


        public async Task<IActionResult> VisualizarMusicas(int id)
        {
            var playlist = await _context.Playlists.Include(p => p.Musicas)
                 .FirstOrDefaultAsync(p => p.PlaylistId == id);

            if (playlist == null)
            {
                return NotFound();
            }
            return View(playlist);
        }

    }

}


