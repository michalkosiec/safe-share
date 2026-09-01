using SafeShare.Domain.Entities;

namespace SafeShare.Application.Features.Groups.UpdateGroup;

public record UpdateGroupCommand(Guid Id, string Name, Guid OwnerId);