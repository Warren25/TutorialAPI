namespace UserApi.Services;

public class CounterService : ICounterService
{
    private int _count = 0;
    
    public int GetCount() => _count;
    
    public void Increment() => _count++;
}