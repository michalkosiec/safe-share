using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeShare.Application.Common.Interfaces;
using SafeShare.Application.Features.Files.GenerateDownloadUrl;
using SafeShare.Application.Features.Files.GenerateUploadUrl;
using Wolverine;

namespace SafeShare.Api.Controllers;

[Authorize]
[ApiController]
[Route("/api/files")]
public class FilesController(IMessageBus bus, ICurrentUserService currentUserService): ControllerBase
{
    [HttpPost("upload-url")]
    public async Task<IActionResult> GenerateUploadUrl([FromBody] GenerateUploadUrlRequest request, CancellationToken cancellationToken)
    {
        
        var ownerId = currentUserService.UserId;

        var command = new GenerateUploadUrlCommand(request.FileName, request.ContentType, ownerId);

        var uploadUrl = await bus.InvokeAsync<string>(command, cancellationToken);
        
        return Ok(new { Url = uploadUrl });
    }

    [HttpGet("{fileId:Guid}/download-url")]
    public async Task<IActionResult> GenerateDownloadUrl([FromRoute] Guid fileId, CancellationToken cancellationToken)
    {
        var ownerId = currentUserService.UserId;
        
        var command = new GenerateDownloadUrlCommand(fileId, ownerId);
        var downloadUrl = await bus.InvokeAsync<string>(command, cancellationToken);
        
        return Ok(new { Url = downloadUrl });
    }
    
    public record GenerateUploadUrlRequest(string FileName, string ContentType);
}