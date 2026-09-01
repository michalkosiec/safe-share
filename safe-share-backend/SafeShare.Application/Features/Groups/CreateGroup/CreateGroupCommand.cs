using SafeShare.Domain.Entities;

namespace SafeShare.Application.Features.Groups.CreateGroup;

public record CreateGroupCommand(string Name, Guid OwnerId);