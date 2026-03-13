using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Auth;
using EgyptVoyage.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EgyptVoyage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClerkController : ControllerBase
{
    private readonly IClerkRepository _clerkRepository;
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    private readonly PasswordHasher _passwordHasher;

    public ClerkController(
        IClerkRepository clerkRepository,
        JwtTokenGenerator jwtTokenGenerator,
        PasswordHasher passwordHasher)
    {
        _clerkRepository = clerkRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
    }

    // POST: api/clerk/login
    // الـ Clerk بيعمل Login ويرجعله JWT Token بـ Role = "Clerk"
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] ClerkLoginDto request)
    {
        try
        {
            var clerk = await _clerkRepository.GetByEmailAsync(request.Email);

            if (clerk == null || !_passwordHasher.VerifyPassword(request.Password, clerk.Password))
                return Unauthorized(new { message = "Invalid email or password" });

            var token = _jwtTokenGenerator.GenerateTokenForClerk(clerk);
            return Ok(new
            {
                token,
                id = clerk.Id,
                name = clerk.Name,
                email = clerk.Email,
                role = "Clerk"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred", error = ex.Message });
        }
    }

}
