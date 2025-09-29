namespace UserApi.Services;

public class GuidService : IGuidService
{
    private readonly Guid _guid;
    
    public GuidService()
    {
        _guid = Guid.NewGuid();
        Console.WriteLine($"GuidService created with ID: {_guid}");
    }
    
    public Guid GetGuid() => _guid;
}