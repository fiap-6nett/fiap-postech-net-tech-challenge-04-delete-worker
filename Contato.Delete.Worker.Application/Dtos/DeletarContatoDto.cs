using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Contato.Delete.Worker.Application.Dtos
{
    public class DeletarContatoDto
    {
        [Required(ErrorMessage = "Id é obrigatório.")]
        public Guid Id { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
