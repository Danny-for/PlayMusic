using System.ComponentModel.DataAnnotations;

namespace PlayMusic.Models
{
    /// <summary>
    /// <Para>Resumo: Class responsavel por representar a Playlist no banco de dado.</Para>
    /// <Para>Criador por: Dannyela Souza</Para>
    /// <Para>15/09/2024</Para>
    /// </summary>
    public class Playlist
    {

        public Playlist()
        {
            Musicas = new List<Musica>(); // Inicializa a coleção para evitar erros de null
        }

        public int PlaylistId { get; set; }
        public string UsuarioId { get; set; } // Chave estrangeira

        [Required]
        public string PlaylistNome { get; set; }

        public Usuario Usuario { get; set; } // Relacionamento com a tabela de usuários
        public ICollection<Musica> Musicas { get; set; } // Navegação para músicas


    }
}
