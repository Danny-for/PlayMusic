using System.ComponentModel.DataAnnotations;

namespace PlayMusic.ViewModels
{
    /// <summary>
    /// ViewModel responsável pelos dados de entrada no formulário de login.
    /// Esta classe contém as informações necessárias para o login de um usuário,
    /// como email, senha e a opção de lembrar o login.
    /// </summary>
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
