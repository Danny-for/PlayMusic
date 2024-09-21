using System.ComponentModel.DataAnnotations;

namespace PlayMusic.ViewModels
{
    /// <summary>
    /// ViewModel responsável pelos dados de entrada no formulário de registro de usuário.
    /// Esta classe contém as informações necessárias para registrar um novo usuário, 
    /// como nome, email, senha e a confirmação da senha.
    /// </summary>
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "Campo nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Email é obrigatório!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Senha é obrigatório!")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "O {0} deve ter no máximo {2} e no máximo {1} caracteres de comprimento.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "As senhas não Conferem.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Campo Confirmar Senha é obrigatório!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
