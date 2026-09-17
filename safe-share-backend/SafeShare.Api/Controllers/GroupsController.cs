using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeShare.Application.Common.Interfaces;
using SafeShare.Application.Features.Groups.CreateGroup;
using SafeShare.Application.Features.Groups.DeleteGroup;
using SafeShare.Application.Features.Groups.DTOs;
using SafeShare.Application.Features.Groups.GetAllGroups;
using SafeShare.Application.Features.Groups.GetGroup;
using SafeShare.Application.Features.Groups.UpdateGroup;
using Wolverine;

namespace SafeShare.Api.Controllers;

[Authorize]
[ApiController]
[Route("/api/groups")]

public class GroupsController(IMessageBus bus, ICurrentUserService currentUserService): ControllerBase
{
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        var command = new GetGroupQuery(id, userId); 
        var groupResponse = await bus.InvokeAsync<GroupResponse>(command, cancellationToken);
        return Ok(groupResponse);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        var command = new GetAllGroupsQuery(userId);
        var groupsResponse = await bus.InvokeAsync<IEnumerable<GroupResponse>>(command, cancellationToken);
        return Ok(groupsResponse);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] GroupCreateRequest request, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        var command = new CreateGroupCommand(request.Name, userId);
        
        var groupResponse = await bus.InvokeAsync<GroupResponse>(command, cancellationToken);
        return Created($"/api/groups/{request.Name}", groupResponse);
    }
    
     [HttpPut("{id:Guid}")] 
    public async Task<IActionResult> PutAsync(Guid id, [FromBody] GroupUpdateRequest request, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        var command = new UpdateGroupCommand(id, request.Name, userId); 
        var groupResponse = await bus.InvokeAsync<GroupResponse>(command, cancellationToken);
        
        return  Ok(groupResponse);
    } 
    
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        var command = new DeleteGroupCommand(id, userId);
        await bus.InvokeAsync(command, cancellationToken);
        
        return NoContent();
    }
    
    public record GroupCreateRequest(string Name);
    public record GroupUpdateRequest(string Name);
}