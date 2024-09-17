using Microsoft.EntityFrameworkCore;
using PlayMusic.Models;

namespace PlayMusic.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}

       public DbSet<Usuario> Usuarios { get; set; }
       public DbSet<Musica> Musicas { get; set; }
       public DbSet<Playlist> Playlists {  get; set; }

    }
}
