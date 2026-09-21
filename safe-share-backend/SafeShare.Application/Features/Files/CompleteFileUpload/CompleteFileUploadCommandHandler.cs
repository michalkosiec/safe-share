using SafeShare.Domain.Entities;
using SafeShare.Domain.Repositories;

namespace SafeShare.Application.Features.Files.CompleteFileUpload;

public class CompleteFileUploadCommandHandler(ISharedFileRepository repo)
{
    public async Task HandleAsync(CompleteFileUploadCommand request, CancellationToken cancellationToken)
    {
        var file = await repo.GetAsync(request.Id, request.UserId, cancellationToken);
        if (file == null)
            throw new KeyNotFoundException($"File with id {request.Id} not found");
        file.Status = SharedFileStatus.Available;
    }
}