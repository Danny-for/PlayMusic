using System.ComponentModel.DataAnnotations;

namespace PlayMusic.ViewModels
{
    /// <summary>
    /// ViewModel responsável pela alteração de senha do usuário.
    /// Esta classe contém as informações necessárias para alterar a senha,
    /// como email, nova senha e a confirmação da nova senha.
    /// </summary>
    public class AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Senha é obrigatório!")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "O {0} deve ter no máximo {2} e no máximo {1} caracteres de comprimento.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [Compare("ConfirmNewPassword", ErrorMessage = "As senhas não Conferem.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
