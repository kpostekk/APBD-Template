using WebApp_Template.DTOs;

namespace WebApp_Template.Services;

public interface IKeyValueService
{
    public Task<KvResult?> Get(string key);
    public Task Put(string key, string value);
    public Task<bool> Delete(string key);
}