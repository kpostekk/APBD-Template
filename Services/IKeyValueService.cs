namespace WebApp_Template.Services;

public interface IKeyValueService
{
    public Task<string?> Get(string key);
    public Task Put(string key, string value);
    public Task<bool> Delete(string key);
}