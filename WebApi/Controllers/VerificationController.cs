using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VerificationController(DataContext context, UserManager<IdentityUser> userManager) : ControllerBase
{
    // Det görs en instans av DataContext. Den används för att interagera med databasen, för att hämta och spara.
    private readonly DataContext _context = context;

    // Det görs en instans av UserManager <IdentityUser>. Den hanterar användar autentisering.
    // UserManager används för att hantera användare, skapa, hämta, uppdatera och radera användare.

    //UserManagaer är en del av ASP.NET Identity FrameWork
    private readonly UserManager<IdentityUser> _userManager = userManager;

    // Denna HttpPost tar emot en Verification model som inmatning. 
    [HttpPost]
    public async Task<IActionResult> Verify(Verification model) // Den här går mot AccountVerfication Model.
    {
        if (ModelState.IsValid)
        {
            // Här hämtas den första  förekomsten av AccountVerification från databasen som matchar den inmatade emailen.
            var verficationCode = await _context.AccountVerfications.FirstOrDefaultAsync(x => x.Email == model.Email);

            // Kontrollerar att verificationCode inte är null och att den inte har löpt ut.
            if (verficationCode != null && verficationCode.ExpiresAt > DateTime.Now)
            {
                if (verficationCode.VerficationCode == model.VerficationCode)
                {
                    // Hämtar den första användaren som matchar den angivna emailen i model.Email
                    var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

                    // Om användaren hitas och inte är null så ställs EmailConfirmed i databasen till True.
                    // Vilket betyder att emailen har verifierats
                    if(user != null)
                    {
                        user.EmailConfirmed = true;
                        // Användaren uppdateras i databasen och sparar ändringen av EmailConfirmed tabellen i databsen
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            return Ok();
                        }
                    }
                }
            }
        }
        // Om villkoren inte uppfylls returneras BadRequest();
        return BadRequest();
    }
}
