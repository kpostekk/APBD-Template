using Microsoft.AspNetCore.Mvc;
using WebApp_Template.DTOs;
using WebApp_Template.Services;

namespace WebApp_Template.Controllers;

[Route("api/kv")]
[ApiController]
public class KeyValueController : ControllerBase
{
    private readonly IKeyValueService _keyValueService;
    
    public KeyValueController(IKeyValueService keyValueService)
    {
        _keyValueService = keyValueService;
    }
    
    [HttpGet("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(KvResult))]
    public async Task<IActionResult> Get(string key)
    {
        var result = await _keyValueService.Get(key);
        if (result is null) return NotFound();
        return Ok(result);
    }
    
    [HttpPut("{key}")]
    public async Task<IActionResult> Put(string key, [FromBody] string value)
    {
        await _keyValueService.Put(key, value);
        return Ok();
    }
    
    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete(string key)
    {
        var result = await _keyValueService.Delete(key);
        if (!result) return NotFound();
        return Ok();
    }
}