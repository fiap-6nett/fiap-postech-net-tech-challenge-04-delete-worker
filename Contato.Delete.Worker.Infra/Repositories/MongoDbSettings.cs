namespace Contato.Delete.Worker.Infra.Repositories
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null;
        public string Database { get; set; } = null;
        public string CollectionName { get; set; } = null!;
    }
}
