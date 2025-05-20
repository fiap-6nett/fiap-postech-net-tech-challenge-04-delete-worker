using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Contato.Delete.Worker.Domain.Entities
{
    public class ContatoEntity
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public string Ddd { get; private set; }

        public ContatoEntity()
        {
            Id = Guid.NewGuid();
        }


        #region  validações

        public void SetId(Guid id)
        {
            try
            {
                Id = id;
            }
            catch
            {
                throw new ArgumentException("Id invalido");
            }

        }

        #endregion

    }
}
