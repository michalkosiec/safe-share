using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeShare.Application.Common.Interfaces;
using SafeShare.Application.Features.Files.CompleteFileUpload;
using SafeShare.Application.Features.Files.DTOs;
using SafeShare.Application.Features.Files.GenerateDownloadUrl;
using SafeShare.Application.Features.Files.GenerateUploadUrl;
using SafeShare.Application.Features.Files.GetFileStats;
using SafeShare.Domain.ValueObjects;
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
        var userId = currentUserService.UserId;

        var command = new GenerateUploadUrlCommand(request.FileName, request.ContentType, request.Size, userId);
        var response = await bus.InvokeAsync<GenerateUploadUrlResponse>(command, cancellationToken);
        
        return Ok(new { Url = response.Url, Id = response.Id });
    }

    [HttpGet("{fileId:Guid}/download-url")]
    public async Task<IActionResult> GenerateDownloadUrl([FromRoute] Guid fileId, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        
        var command = new GenerateDownloadUrlCommand(fileId, userId);
        var downloadUrl = await bus.InvokeAsync<string>(command, cancellationToken);
        
        return Ok(new { Url = downloadUrl });
    }

    [HttpPost("{fileId:Guid}/complete-upload")]
    public async Task<IActionResult> Complete([FromRoute] Guid fileId, CancellationToken cancellationToken)
    {
        var userId =  currentUserService.UserId;
        
        var command = new CompleteFileUploadCommand(fileId, userId);
        await bus.InvokeAsync(command, cancellationToken);
        
        return NoContent();
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats(CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        
        var query = new GetFileStatsQuery(userId);
        var stats = await bus.InvokeAsync<FileStats>(query, cancellationToken);
        
        return Ok(new { Stats = stats });
    }
    
    public record GenerateUploadUrlRequest(string FileName, string ContentType, long Size);
}