namespace SafeShare.Domain.Entities;

public class Group
{
    private readonly List<GroupUser> _users = new();
    public Guid Id { get; private set; }
    public Guid OwnerId { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyCollection<GroupUser> Users => _users.AsReadOnly();
    
    private Group() {}

    public Group(string name, User createdByUser)
    {
        Id = Guid.NewGuid();
        Name = name;
        OwnerId = createdByUser.Id;
        _users.Add(new GroupUser(Id, createdByUser.Id));
    }

    public void ChangeOwnership(Guid ownerId)
    {
        OwnerId = ownerId;
    }

    public void AddUser(User user)
    {
        var groupUser = new GroupUser(Id, user.Id);
        _users.Add(groupUser);
    }

    public void RemoveUser(User user)
    {
        var userToRemove = _users.FirstOrDefault(gu => gu.UserId == user.Id);
        
        if (userToRemove == null)
            throw new InvalidOperationException($"User with id {user.Id} does not exist.");
        
        _users.Remove(userToRemove);
    }
}