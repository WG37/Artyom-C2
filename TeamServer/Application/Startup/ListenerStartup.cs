
using TeamServer.Application.Services.ListenerServices.HttpListenerService.HttpCRUD;

namespace TeamServer.Application.Startup
{
    public sealed class ListenerStartup : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ListenerStartup(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var crud = scope.ServiceProvider.GetRequiredService<IHttpCRUD>();

            var listeners = await crud.GetAllListenersAsync();
            foreach (var l in listeners)
            {
                l.Idle();
            }

            await crud.UpdateChangesAsync(listeners);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
