using System.ComponentModel.DataAnnotations;

namespace WebApp_Template.DTOs;

public class KvResult
{
    [Required]
    public string Key { get; init; }
    [Required]
    public string Value { get; init; }
    [Required]
    public DateTime Created { get; init; }
}