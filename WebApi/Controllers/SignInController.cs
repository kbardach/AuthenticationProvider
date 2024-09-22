using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/auth/[controller]")]
[ApiController]
public class SignInController(SignInManager<IdentityUser> signInManager) : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;

    [HttpPost]
    public async Task<IActionResult> SignIn(SignIn model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                // Här bör det vara en AccessToken(Ska byggas)
                // Lägga till en kontroll om det redan finns en användare med den Emailen?
                // Lgga till en Remember me
                return Ok();
            }

            return Unauthorized();
        }

        return BadRequest();
    }
}
