namespace Contato.Delete.Worker.Infra.Mensageria.Consumer
{
    public interface IContatoConsumer
    {
        void StartConsuming(CancellationToken cancellationToken);
    }
}
