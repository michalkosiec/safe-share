namespace SafeShare.Domain.Entities;

public class SharedFile
{
    public Guid Id { get; private set; }
    public Guid OwnerId  { get; private set; }
    public string Path  { get; private set; }

    public SharedFile(Guid ownerId, string path)
    {
        Id = Guid.NewGuid();
        OwnerId = ownerId;
        Path = path;
    }
}