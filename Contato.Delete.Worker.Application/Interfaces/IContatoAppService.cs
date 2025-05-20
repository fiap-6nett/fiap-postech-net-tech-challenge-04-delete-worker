using Contato.Delete.Worker.Application.Dtos;

namespace Contato.Delete.Worker.Application.Interfaces
{
    public interface IContatoAppService
    {
        Task DeletarContato(DeletarContatoDto dto);
    }
}
