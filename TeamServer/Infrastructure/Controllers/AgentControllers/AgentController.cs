using Microsoft.AspNetCore.Mvc;
using TeamServer.Application.Services.AgentServices.AgentCRUD;
using TeamServer.Domain.Entities.Agents;

namespace TeamServer.Infrastructure.Controllers.AgentControllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly IAgentCRUD _agentCRUD;

        public AgentController(IAgentCRUD agentCRUD) => _agentCRUD = agentCRUD;

        [HttpGet]
        public async Task<IActionResult> GetAgents()
        {
            var agents = await _agentCRUD.GetAgentsAsync();
            if (agents == null || !agents.Any())
                return Ok(Array.Empty<Agent>());

            return Ok(agents);
        }

        [HttpGet("{uniqueId}")]
        public async Task<IActionResult> GetAgent(Guid uniqueId)
        {
            if (uniqueId == Guid.Empty)
                return BadRequest($"The id: {uniqueId} is invalid");

            var agent = await _agentCRUD.GetAgentByUniqueIdAsync(uniqueId);
            if (agent == null) 
                return NotFound($"No agent exists with the id: {uniqueId}");

            return Ok(agent);
        }

        [HttpDelete("{uniqueId}")]
        public async Task<IActionResult> RemoveAgent(Guid uniqueId)
        {
            if (uniqueId == Guid.Empty)
                return BadRequest($"The id: {uniqueId} is invalid");

            var deleted = await _agentCRUD.RemoveAgentByUniqueIdAsync(uniqueId);
            if (!deleted)
                return BadRequest($"Cannot delete. No agent with the id: {uniqueId} exists");

            return NoContent();
        }
    }
}
