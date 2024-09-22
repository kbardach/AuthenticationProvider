using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class Verification
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string VerficationCode { get; set; } = null!;
}
