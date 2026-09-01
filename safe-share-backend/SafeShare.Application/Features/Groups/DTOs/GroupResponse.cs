using SafeShare.Domain.Entities;

namespace SafeShare.Application.Features.Groups.DTOs;

public record GroupResponse(Guid Id, string Name, Guid OwnerId);