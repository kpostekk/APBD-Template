using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Get(string key)
    {
        var result = await _keyValueService.Get(key);
        if (result is null) return NotFound();
        return Ok(result);
    }
}