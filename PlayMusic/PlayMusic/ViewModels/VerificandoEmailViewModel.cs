using System.ComponentModel.DataAnnotations;

namespace PlayMusic.ViewModels
{
    /// <summary>
    ///ViewModel responsável pela verificação do email no formulário.
    /// Esta classe contém as informações necessárias para a verificação de um usuário por email.
    /// </summary>
    public class VerificandoEmailViewModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório!")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
