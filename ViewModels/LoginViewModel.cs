using System.ComponentModel.DataAnnotations;

namespace SnackApp.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Informe o usuário")]
    [Display(Name = "Usuário")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Informe a senha")]
    [Display(Name = "Senha")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string ReturnUrl { get; set; }
}