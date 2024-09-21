using Microsoft.AspNetCore.Identity;

namespace PlayMusic.Models
{
    /// <summary>
    /// <para>Class responsavel por criar a tabela Usuario no banco de Dados. </para>
    /// </summary>
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public ICollection<Playlist> Playlists { get; set; }
    }
}
