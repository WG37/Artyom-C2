using TeamServer.Domain.Entities.Agents;

namespace TeamServer.Application.Services.AgentServices.AgentCRUD
{
    public interface IAgentCRUD
    {
        Task AddAgentAsync(Agent agent);
        Task<IEnumerable<Agent>> GetAgentsAsync();
        Task<Agent> GetAgentByUniqueIdAsync(Guid uniqueId);
        Task<bool> RemoveAgentByUniqueIdAsync(Guid uniqueId);
    }
}
