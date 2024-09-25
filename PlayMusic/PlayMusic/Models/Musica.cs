namespace PlayMusic.Models
{
    /// <summary>
    /// <para>Class responsavel por representar a Tabela Musica  o banco de dados.
    /// <para>Criado por : Dannyela Souza</para>
    /// </summary>
    public class Musica
    {
       public int MusicaId { get; set; }
       public int PlaylistId { get; set; }
       public string NomeMusica { get; set; }
       public string ArtistaMusica { get; set; }
       
    }
}
