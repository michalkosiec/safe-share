namespace SafeShare.Domain.Entities;

public enum SharedFileStatus
{
    Pending,
    Available
}

public class SharedFile
{
    public Guid Id { get; private set; }
    public Guid OwnerId  { get; private set; }
    
    public string FileName { get; set; }
    
    public string ContentType { get; set; }
    
    public SharedFileStatus Status { get; set; }
    
    private SharedFile() {}

    public SharedFile(Guid ownerId, string fileName, string contentType)
    {
        Id = Guid.NewGuid();
        OwnerId = ownerId;
        FileName = fileName;
        ContentType = contentType;
        Status = SharedFileStatus.Pending;
    }

    public void MarkAsAvailable()
    {
        Status = SharedFileStatus.Available;
    }
}