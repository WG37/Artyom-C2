using Microsoft.EntityFrameworkCore;
using TeamServer.Domain.Entities.Listeners.HttpListeners;
using TeamServer.Infrastructure.Data;

namespace TeamServer.Application.Services.ListenerServices.HttpListenerService.HttpCRUD
{
    public class HttpCRUD : IHttpCRUD
    {
        private readonly AppDbContext _db;

        public HttpCRUD(AppDbContext db) => _db = db;

        public async Task AddListenerAsync(HttpListenerEntity listener)
        {
            if (listener == null)
                throw new ArgumentNullException(nameof(listener), "Cannot add listener to the database.");

         
            _db.HttpListeners.Add(listener);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<HttpListenerEntity>> GetAllListenersAsync()
        {
            var listeners = await _db.HttpListeners.ToListAsync();
            if (listeners == null)
                throw new InvalidOperationException();

            return listeners;
        }

        public async Task<HttpListenerEntity> GetListenerAsync(string name)
        {
            var listener = await _db.HttpListeners.FirstOrDefaultAsync(l => l.Name == name);

            return listener;
        }

        public async Task<bool> RemoveListenerAsync(string name)
        {
            var listener = await _db.HttpListeners.FirstOrDefaultAsync(l => l.Name == name);

            if (listener == null)
                return false;

            _db.HttpListeners.Remove(listener);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateChangesAsync(HttpListenerEntity listener)
        {
            if (listener == null)
                throw new ArgumentNullException(nameof(listener));

            _db.HttpListeners.Update(listener);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateChangesAsync(IEnumerable<HttpListenerEntity> listeners)
        {
            if (listeners == null)
                throw new ArgumentNullException(nameof(listeners));

            _db.HttpListeners.UpdateRange(listeners);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
