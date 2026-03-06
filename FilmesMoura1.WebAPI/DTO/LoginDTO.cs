using System.ComponentModel.DataAnnotations;

namespace FilmesMoura1.WebAPI.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "O Email do usuario e obrigatorio!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A Senha do usuario e obrigatorio!")]
    public string? Senha { get; set; }
}
