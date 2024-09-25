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

        public IActionResult Index()
        {
            return View();
        }

        // Exibe o formulário para criar uma nova playlist
        [Authorize]
        public IActionResult Criar()
        {
            return View();
        }

        // Cria uma nova playlist
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Criar(Playlist model)
        {
            if (ModelState.IsValid)
            {
                var userIdString = _userManager.GetUserId(User);
                if (userIdString == null)
                {
                    return RedirectToAction("Login", "Account");
                }

              
                var userIdGuid = Guid.Parse(userIdString);
                int userId = BitConverter.ToInt32(userIdGuid.ToByteArray(), 0);

                model.UsuarioId = userId; 
                _context.Playlists.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }

  
        [Authorize]
        public async Task<IActionResult> AdicionarMusica(int id)
        {
            var playlist = _context.Playlists
                .Include(p => p.Musicas)
                .FirstOrDefault(p => p.PlaylistId == id);

            if (playlist == null)
            {
                return NotFound();
            }

            var userIdString = _userManager.GetUserId(User);
            if (userIdString == null)
            {
                return RedirectToAction("Login", "Account");
            }

           
            var userIdGuid = Guid.Parse(userIdString);
            int userId = BitConverter.ToInt32(userIdGuid.ToByteArray(), 0);

            if (userId != playlist.UsuarioId)
            {
                return Forbid();
            }

            return View(playlist);
        }

        // Remover música
        [Authorize]
        public async Task<IActionResult> RemoverMusica(int playlistId, int musicaId)
        {
            var playlist = _context.Playlists
                .Include(p => p.Musicas)
                .FirstOrDefault(p => p.PlaylistId == playlistId);

            if (playlist == null)
            {
                return NotFound();
            }

            var userIdString = _userManager.GetUserId(User);
            if (userIdString == null)
            {
                return RedirectToAction("Login", "Account");
            }

            
            var userIdGuid = Guid.Parse(userIdString);
            int userId = BitConverter.ToInt32(userIdGuid.ToByteArray(), 0);

            if (userId != playlist.UsuarioId)
            {
                return Forbid();
            }

            var musica = playlist.Musicas.FirstOrDefault(m => m.MusicaId == musicaId);
            if (musica != null)
            {
                playlist.Musicas.Remove(musica);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Detalhes", new { id = playlistId });
        }
    }

}


