using Microsoft.AspNetCore.Mvc;
using SafeShare.Application.Features.Groups.CreateGroup;
using SafeShare.Application.Features.Groups.DeleteGroup;
using SafeShare.Application.Features.Groups.DTOs;
using SafeShare.Application.Features.Groups.GetAllGroups;
using SafeShare.Application.Features.Groups.GetGroup;
using SafeShare.Application.Features.Groups.UpdateGroup;
using Wolverine;

namespace SafeShare.Api.Controllers;

[ApiController]
[Route("/api/groups")]

public class GroupsController(IMessageBus bus): ControllerBase
{
    [HttpGet("{id:Guid}")] //Get group
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new GetGroupQuery(id); 
        var groupResponse = await bus.InvokeAsync<GroupResponse>(command, cancellationToken);
        return Ok(groupResponse);
    }
    
    [HttpGet] // Get all groups
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var command = new GetAllGroupsQuery();
        var groupsResponse = await bus.InvokeAsync<IEnumerable<GroupResponse>>(command, cancellationToken);
        return Ok(groupsResponse);
    }
    
    [HttpPost] //Create group
    public async Task<IActionResult> PostAsync([FromBody] GroupCreateRequest request, CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse("8b159e99-72aa-4c9a-be9c-cf10cf645907"); //mocked guid - must replace with CurrentUserService.UserId !!!
        var command = new CreateGroupCommand(request.Name, ownerId);
        
        var groupResponse = await bus.InvokeAsync<GroupResponse>(command, cancellationToken);
        return Created($"/api/groups/{request.Name}", groupResponse);
    }
    
     [HttpPut("{id:Guid}")] // Update group (what about ChangeOwnership?)
    public async Task<IActionResult> PutAsync(Guid id, [FromBody] GroupUpdateRequest request, CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse("8b159e99-72aa-4c9a-be9c-cf10cf645907"); //Replace with CurrentUserService.UserId once implemented. !!!
        var command = new UpdateGroupCommand(id, request.Name, ownerId); 
        var groupResponse = await bus.InvokeAsync<GroupResponse>(command, cancellationToken);
        
        return  Ok(groupResponse);
    } 
    
    [HttpDelete("{id:Guid}")] // Delete group
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteGroupCommand(id);
        await bus.InvokeAsync(command, cancellationToken);
        
        return NoContent();
    }
    
    public record GroupCreateRequest(string Name);
    public record GroupUpdateRequest(string Name);
}