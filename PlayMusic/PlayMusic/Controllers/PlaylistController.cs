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
            var user = await _userManager.GetUserAsync(User);
            var minhasPlaylists = await _context.Playlists
                .Where(p => p.UsuarioId == user.Id)
                .ToListAsync();

            return View(minhasPlaylists);
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
            var user = await _userManager.GetUserAsync(User);
            model.UsuarioId = user.Id;
            model.Usuario = user;


            _context.Playlists.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [AllowAnonymous] // Permite  todos os usuários
        public async Task<IActionResult> TodasPlaylists()
        {
            var todasPlaylists = await _context.Playlists
                .Include(p => p.Usuario)
                .ToListAsync();

            return View(todasPlaylists); 
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
        [Authorize]
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


