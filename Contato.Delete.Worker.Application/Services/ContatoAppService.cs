using Contato.Delete.Worker.Application.Dtos;
using Contato.Delete.Worker.Application.Interfaces;
using Contato.Delete.Worker.Domain.Entities;
using Contato.Delete.Worker.Domain.Interfaces;

namespace Contato.Delete.Worker.Application.Services
{
    public class ContatoAppService : IContatoAppService
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoAppService(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public Task DeletarContato(DeletarContatoDto dto)
        {
            var contato = new ContatoEntity();

            contato.SetId(dto.Id);

            _contatoRepository.DeletarContato(contato);

            return Task.CompletedTask;
        }
    }
}
