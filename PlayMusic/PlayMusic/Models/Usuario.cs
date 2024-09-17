namespace PlayMusic.Models
{
    public class Usuario
    {
        /// <summary>
        /// <para>Class responsavel por criar a tabela Usuario no banco de Dados. </para>
        /// </summary>
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Gmail { get; set; }
        public string Senha { get; set; }
        public ICollection<Playlist> Playlists { get; set; }
    }
}
