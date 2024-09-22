using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contexts;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/auth/[controller]")]
[ApiController]
public class SignUpController(UserManager<IdentityUser> userManager, DataContext context) : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly DataContext _context = context;

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUp model)
    {
        if (ModelState.IsValid)
        {
            var identityUser = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);
            if (result.Succeeded)
            {
                var rnd = new Random();
                var code = rnd.Next(10000, 99999).ToString();

                _context.AccountVerfications.Add(new AccountVerficationEntity{
                    Email = identityUser.Email,
                    VerficationCode = code.ToString()
                });
                await _context.SaveChangesAsync();

                return Created("", null);
            }
        }

        return BadRequest();
    }
}
