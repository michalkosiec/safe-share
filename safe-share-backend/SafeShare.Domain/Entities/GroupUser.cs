namespace SafeShare.Domain.Entities;

public class GroupUser
{
    public Guid GroupId { get; private set; }
    public Guid UserId { get; private set; }

    internal GroupUser(Guid groupId, Guid userId)
    {
        GroupId = groupId;
        UserId = userId;
    }
}