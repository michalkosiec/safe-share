using Microsoft.AspNetCore.Mvc;
using SafeShare.Application.Features.Groups.CreateGroup;
using SafeShare.Application.Features.Groups.DeleteGroup;
using SafeShare.Application.Features.Groups.DTOs;
using SafeShare.Application.Features.Groups.GetAllGroups;
using SafeShare.Application.Features.Groups.GetGroup;
using SafeShare.Application.Features.Groups.UpdateGroup;
using SafeShare.Domain.Entities;
using Wolverine;

namespace SafeShare.Api.Controllers;

[ApiController]
[Route("/api/groups")]

public class GroupsController(IMessageBus bus): ControllerBase
{
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new GetGroupQuery(id); 
        var groupResponse = await bus.InvokeAsync<GroupResponse>(command, cancellationToken);
        return Ok(groupResponse);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var command = new GetAllGroupsQuery();
        var groupsResponse = await bus.InvokeAsync<IEnumerable<GroupResponse>>(command, cancellationToken);
        return Ok(groupsResponse);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] GroupCreateRequest request, CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse("[guid-here]"); //mocked guid - must replace with CurrentUserService.UserId
        var command = new CreateGroupCommand(request.Name, ownerId);
        
        var groupResponse = await bus.InvokeAsync<GroupResponse>(command, cancellationToken);
        return Created($"/api/groups/{request.Name}", groupResponse);
    }
    
    /* [HttpPut("{id:Guid}")]
    public async Task<IActionResult> PutAsync(Guid id, [FromBody] GroupUpdateRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateGroupCommand(id, request.Name, OwnerId); //resolve this
        var groupResponse = await bus.InvokeAsync<GroupResponse>(command, cancellationToken);
        
        return  Ok(groupResponse);
    } */
    
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteGroupCommand(id);
        await bus.InvokeAsync(command, cancellationToken);
        
        return NoContent();
    }
    
    public record GroupCreateRequest(string Name);
    public record GroupUpdateRequest(string Name);
}