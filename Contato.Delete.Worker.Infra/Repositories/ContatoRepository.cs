using Contato.Delete.Worker.Domain.Entities;
using Contato.Delete.Worker.Domain.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Contato.Delete.Worker.Infra.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly IMongoCollection<ContatoEntity> _contato;

        public ContatoRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> mongoDbSettings)
        {
            var database = mongoClient.GetDatabase(mongoDbSettings.Value.Database);
            _contato = database.GetCollection<ContatoEntity>("contatos");
        }

        public void DeletarContato(ContatoEntity contato)
        {
            try
            {

                // Criando um filtro para buscar pelo Id
                var filterId = Builders<ContatoEntity>.Filter.Eq(c => c.Id, contato.Id);

                // Realizando a busca no banco
                var existingContato = _contato.Find(filterId).FirstOrDefault();


                if (existingContato == null)
                {
                    throw new Exception("Contato não encontrado.");
                }

                var filter = Builders<ContatoEntity>.Filter.Eq(c => c.Id, contato.Id);


                _contato.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao deletar o contato. Erro {ex.Message}");
            }
        }

    }
}
