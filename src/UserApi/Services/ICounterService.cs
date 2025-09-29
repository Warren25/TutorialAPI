namespace UserApi.Services;

public interface ICounterService
{
    int GetCount();
    void Increment();
}