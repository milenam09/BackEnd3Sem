using System.ComponentModel.DataAnnotations;

namespace ConnectPlus.DTO
{
    public class ContatoDTO
    {
        public Guid IdContato { get; set; }

        [Required(ErrorMessage = "O nome do contato é obrigatório.")]

        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "Os dados de contato são obrigatórios.")]
        public string? DadosContato { get; set; }

        public IFormFile? Imagem { get; set; }

        [Required(ErrorMessage = "O tipo de contato deve ser informado.")]
        public Guid? IdTipoContato { get; set; }
    }
}
