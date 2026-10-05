using SafeShare.Domain.Repositories;
using SafeShare.Domain.ValueObjects;

namespace SafeShare.Application.Features.Files.GetFileStats;

public class GetFileStatsQueryHandler(ISharedFileRepository repo)
{
    public async Task<FileStats> HandleAsync(GetFileStatsQuery query, CancellationToken cancellationToken)
    {
        return await repo.GetStatsAsync(query.UserId, cancellationToken);
    }
}