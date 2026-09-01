namespace SafeShare.Domain.Entities;

public class Group
{
    private readonly List<GroupUser> _users = new();
    public Guid Id { get; private set; }
    public Guid OwnerId { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyCollection<GroupUser> Users => _users.AsReadOnly();
    
    private Group() {}

    public Group(string name, Guid ownerId)
    {
        Id = Guid.NewGuid();
        Name = name;
        OwnerId = ownerId;
        _users.Add(new GroupUser(Id, ownerId));
    }

    public void ChangeOwnership(Guid ownerId)
    {
        OwnerId = ownerId;
    }

    public void AddUser(Guid userId)
    {
        var groupUser = new GroupUser(Id, userId);
        _users.Add(groupUser);
    }

    public void RemoveUser(Guid userId)
    {
        var userToRemove = _users.FirstOrDefault(gu => gu.UserId == userId);
        
        if (userToRemove == null)
            throw new InvalidOperationException($"User with id {userId} does not exist.");
        
        _users.Remove(userToRemove);
    }
}