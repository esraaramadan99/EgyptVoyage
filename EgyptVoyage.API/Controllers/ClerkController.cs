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

            // مؤقت للـ Debug - احذفيه بعدين
            if (clerk == null)
                return BadRequest(new { message = "Clerk not found in DB", email = request.Email });

            var isPasswordValid = _passwordHasher.VerifyPassword(request.Password, clerk.Password);

            // مؤقت للـ Debug - احذفيه بعدين
            if (!isPasswordValid)
                return BadRequest(new { message = "Password wrong", enteredPassword = request.Password, storedHash = clerk.Password });

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
    [HttpGet("debug")]
    public async Task<IActionResult> Debug()
    {
        try
        {
            var all = await _clerkRepository.GetAllAsync();
            return Ok(new
            {
                count = all.Count,
                clerks = all.Select(x => new { x.Id, x.Email, x.Name })
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
