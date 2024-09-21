namespace PlayMusic.Models
{
    /// <summary>
    /// <Para>Resumo: Class responsavel por representar a Playlist no banco de dado.</Para>
    /// <Para>Criador por: Dannyela Souza</Para>
    /// <Para>15/09/2024</Para>
    /// </summary>
    public class Playlist
    {
 
        public int PlaylistId { get; set; }
        public int UsuarioId { get; set; }//Chave estrangeira
        public string PlaylistNome { get; set; }
        public Usuario usuarios { get; set; } //Propriedade de navegação
        public ICollection<Musica> Musicas { get; set; }//propriedade de navegação tbm.
        

    }
}
