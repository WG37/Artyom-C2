using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using TeamServer.Domain.Entities.Agents;
using TeamServer.Infrastructure.Data;

namespace TeamServer.Application.Services.AgentServices.AgentCRUD
{
    public class AgentCRUD : IAgentCRUD
    {
        private readonly AppDbContext _db;

        public AgentCRUD(AppDbContext db) => _db = db;

        
        public async Task AddAgentAsync(Agent agent)
        {
            if (agent == null)
                throw new ArgumentNullException(nameof(agent), "Cannot add a null entity.");
           
            _db.Agents.Add(agent);
             await _db.SaveChangesAsync();
        }
       
        public async Task<IEnumerable<Agent>> GetAgentsAsync()
        {
            var agents = await _db.Agents.ToListAsync();
            if (agents == null)
                throw new InvalidOperationException("No entities exist in database.");

            return agents;
        }
        
        public async Task<Agent> GetAgentByUniqueIdAsync(Guid uniqueId)
        {
            if (uniqueId == Guid.Empty)
                throw new ArgumentException($"The uniqueId: '{uniqueId}' is invalid.");

            return await _db.Agents
                .Include(a => a.Metadata)
                .FirstOrDefaultAsync(a => a.UniqueId == uniqueId);
        }
       
        public async Task<bool> RemoveAgentByUniqueIdAsync(Guid uniqueId)
        {
            if (uniqueId == Guid.Empty)
                throw new ArgumentException($"The id: '{uniqueId}' is invalid.");

            var agent = await _db.Agents
                .Include(a => a.Metadata)
                .FirstOrDefaultAsync(a => a.UniqueId == uniqueId);

            if (agent == null)
                return false;

            _db.Agents.Remove(agent);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
