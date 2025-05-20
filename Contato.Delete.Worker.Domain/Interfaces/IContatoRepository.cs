using Contato.Delete.Worker.Domain.Entities;

namespace Contato.Delete.Worker.Domain.Interfaces
{
    public interface IContatoRepository
    {
        public void DeletarContato(ContatoEntity contato);      
    }
}
