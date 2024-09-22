using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities;


// Den här gör vi en tabell av, den ska reg. i DataContext. Sen Add-Migration -> Update-Database
public class AccountVerficationEntity
{
    [Key]
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string VerficationCode { get; set; } = null!;

    // Den här gör så att den är giltig 5min från att den skapas, 
    public DateTime ExpiresAt { get; set; } = DateTime.Now.AddMinutes(5);
}
